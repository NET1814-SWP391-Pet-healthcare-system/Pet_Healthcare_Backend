using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetVaccines()
        {
            var vaccineList = await _vaccineService.GetAllVaccines();
            return Ok(vaccineList);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccineById(int id)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vaccine = await _vaccineService.GetVaccineById(id);
            if(vaccine == null)
            {
                return NotFound("Vaccine not found");
            }
            return Ok(vaccine);
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddVaccine(VaccineAddRequest vaccineAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vaccine = await _vaccineService.AddVaccine(vaccineAddRequest);
            return Ok(vaccine);
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccineById(int id)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vaccine = await _vaccineService.GetVaccineById(id);
            if (vaccine == null)
            {
                return NotFound("Vaccine not found");
            }
            var isDeleted = await _vaccineService.RemoveVaccine(id);
            if(!isDeleted)
            {
                return BadRequest("Vaccine deletion failed");
            }
            return Ok(vaccine);
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            if(id != vaccineUpdateRequest.VaccineId)
            {
                return BadRequest("Mismatched Id");
            }
            
            var result = await _vaccineService.UpdateVaccine(id, vaccineUpdateRequest);
            if(result == null)
            {
                return NotFound(vaccineUpdateRequest);
            }
            return Ok(result);
        }

    }
}
