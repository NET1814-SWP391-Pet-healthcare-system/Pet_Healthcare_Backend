using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.VaccineDTO;
using System.Net;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccineController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [HttpGet]
        public async Task<ActionResult<BusinessResult>> GetVaccines()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Status = 200;
            businessResult.Message = "Get all vaccines successfully";
            businessResult.Data = await _vaccineService.GetAllVaccines();
            return Ok(businessResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessResult>> GetVaccineById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var vaccine = await _vaccineService.GetVaccineById(id);
            if(vaccine == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Vaccine not found";
                businessResult.Data = false;
            }
            businessResult.Status = 200;
            businessResult.Message = "Vaccine found";
            businessResult.Data = vaccine;
            return Ok(businessResult);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessResult>> AddVaccine(VaccineAddRequest vaccineAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var vaccine = await _vaccineService.AddVaccine(vaccineAddRequest);
            businessResult.Status = 200;
            businessResult.Message = "Vaccine added";
            businessResult.Data = vaccine;
            return Ok(businessResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessResult>> DeleteVaccineById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var vaccineData = await _vaccineService.GetVaccineById(id);
            if (vaccineData == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Vaccine not found";
                businessResult.Data = false;
                return NotFound(businessResult);
            }
            var isDeleted = await _vaccineService.RemoveVaccine(id);
            if(!isDeleted)
            {
                businessResult.Status = 400;
                businessResult.Message = "Didn't delete";
                businessResult.Data = vaccineData;
                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Vaccine deleted";
            businessResult.Data = vaccineData;
            return Ok(businessResult);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BusinessResult>> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            if(id != vaccineUpdateRequest.VaccineId)
            {
                businessResult.Status = 400;
                businessResult.Message = "Mismatched id";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            
            var result = await _vaccineService.UpdateVaccine(id, vaccineUpdateRequest);
            if(result == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Vaccine not found";
                businessResult.Data = vaccineUpdateRequest;
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Vaccine updated";
            businessResult.Data = result;
            return Ok(businessResult);
        }

    }
}
