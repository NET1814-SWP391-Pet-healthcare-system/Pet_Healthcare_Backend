using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts.Mappers;

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
        public ActionResult<BusinessResult> AddHospitalization([FromQuery] int petId, int kennelId, int vetId,double totalCost, [FromBody] HospitalizationAddRequest hospitalizationAddRequest)
        {
            if (hospitalizationAddRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _hospitalizationService.AddHospitalization(hospitalizationAddRequest.ToHospitalizationFromAdd(petId, kennelId, vetId, totalCost));
            return Ok("Added Hospitalization Successfully");
        }

        //Read
        [HttpGet]
        public ActionResult<BusinessResult> GetHospitalizations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hospitalizations = _hospitalizationService.GetHospitalizations();
            var hospitalizationDtos = hospitalizations.Select(x => x.ToHospitalizationDto());
            return Ok(hospitalizationDtos);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetHospitalizationById(int id)
        {
            var hospitalization = _hospitalizationService.GetHospitalizationById(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (hospitalization == null)
            {
                return NotFound();
            }
            return Ok(hospitalization);
        }


        //Update
        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdateHospitalizationById(int id, Hospitalization? hospitalizationUpdateRequest)
        {

            if (hospitalizationUpdateRequest == null || !ModelState.IsValid || id != hospitalizationUpdateRequest.HospitalizationId)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _hospitalizationService.UpdateHospitalization(id,hospitalizationUpdateRequest);
            if (!isUpdated)
            {
                return BadRequest(ModelState);
            }
            return Ok("Updated Successfully");
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<BusinessResult> DeleteHospitalizationByHospitalizationByID(int id)
        {
            var hospitalizationData = _hospitalizationService.GetHospitalizationById(id);
            var isDeleted = _hospitalizationService.RemoveHospitalization(id);
            if (!ModelState.IsValid || !isDeleted)
            {
                return BadRequest(ModelState);
            }
            return Ok("Nuke Success");
        }
    }
}
