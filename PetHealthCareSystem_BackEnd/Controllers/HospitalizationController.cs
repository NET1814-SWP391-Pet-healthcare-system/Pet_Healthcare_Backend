using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.HospitalizationDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalizationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHospitalizationService _hospitalizationService;

        public HospitalizationController(ApplicationDbContext context, IHospitalizationService hospitalizationService)
        {
            _context = context;
            _hospitalizationService = hospitalizationService;
        }

        //Create
        [HttpPost]
        public ActionResult<BusinessResult> AddHospitalization(HospitalizationAddRequest? hospitalizationAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (hospitalizationAddRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Hospitalization request is null";
                return BadRequest(businessResult);
            }

            _hospitalizationService.AddHospitalization(hospitalizationAddRequest);
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No Hospitalization found";
            return Ok(businessResult);
        }

        //Read
        [HttpGet]
        public ActionResult<BusinessResult> GetHospitalizations()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _hospitalizationService.GetHospitalizations(); ;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetHospitalizationById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var hospitalization = _hospitalizationService.GetHospitalizationById(id);
            if (hospitalization == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Hospitalization found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = hospitalization;
            businessResult.Message = "Hospitalization found";
            return Ok(businessResult);
        }


        //Update
        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdateHospitalizationById(int id, HospitalizationUpdateRequest? hospitalizationUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (hospitalizationUpdateRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            if (id != hospitalizationUpdateRequest.HospitalizationId)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Mismatched id";
                return BadRequest(businessResult);
            }
            var isUpdated = _hospitalizationService.UpdateHospitalization(hospitalizationUpdateRequest);
            if (!isUpdated)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Hospitalization not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = hospitalizationUpdateRequest.ToHospitalization();
            businessResult.Message = "Hospitalization updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{hospitalizationname}")]
        public ActionResult<BusinessResult> DeleteHospitalizationByHospitalizationname(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var hospitalizationData = _hospitalizationService.GetHospitalizationById(id);
            var isDeleted = _hospitalizationService.RemoveHospitalization(id);
            if (!isDeleted)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Hospitalization not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = hospitalizationData;
            businessResult.Message = "Hospitalization deleted";
            return Ok(businessResult);
        }
    }
}
