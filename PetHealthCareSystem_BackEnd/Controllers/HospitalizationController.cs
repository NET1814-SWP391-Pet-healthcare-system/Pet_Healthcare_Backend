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
        public async Task<IActionResult> AddHospitalization([FromBody] HospitalizationAddRequest hospitalizationAddRequest)
        {
            if (hospitalizationAddRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _hospitalizationService.AddHospitalization(hospitalizationAddRequest.ToHospitalizationFromAdd());
            return Ok("Added Hospitalization Successfully");
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetHospitalizations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hospitalizations = await _hospitalizationService.GetHospitalizations();
            var hospitalizationDtos = hospitalizations.Select(x => x.ToHospitalizationDto());
            return Ok(hospitalizationDtos);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetHospitalizationById(int id)
        {
            var hospitalization =await _hospitalizationService.GetHospitalizationById(id);
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
        public async Task<IActionResult> UpdateHospitalization(int id, [FromBody] HospitalizationUpdateRequest hospitalizationUpdateRequest)
        {
            var existingHospitalization = await _hospitalizationService.GetHospitalizationById(id);
            if (hospitalizationUpdateRequest == null || !ModelState.IsValid || existingHospitalization== null)
            {
                return BadRequest(ModelState);
            }
            var isUpdated  =await _hospitalizationService.UpdateHospitalization(id, hospitalizationUpdateRequest.ToHospitalizationUpdate());
            if (isUpdated==null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Updated Successfully");
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalizationByHospitalizationByID(int id)
        {
            var isDeleted = await _hospitalizationService.RemoveHospitalization(id);
            if (!ModelState.IsValid || isDeleted==null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Nuke successfully");
        }
    }
}
