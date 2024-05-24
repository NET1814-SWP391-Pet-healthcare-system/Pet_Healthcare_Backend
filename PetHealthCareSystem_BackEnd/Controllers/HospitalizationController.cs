using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.HospitalizationDTO;
using Services;

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

        [HttpPost]
        public IActionResult AddHospitalization(HospitalizationAddRequest? HospitalizationAddRequest)
        {
            if (HospitalizationAddRequest == null)
            {
                return BadRequest("HospitalizationRequest is null");
            }

            _hospitalizationService.AddHospitalization(HospitalizationAddRequest);

            return Ok("Created successfully");
        }
        public IActionResult UpdateHospitalization(HospitalizationUpdateRequest? HospitalizationUpdateRequest)
        {
            if (HospitalizationUpdateRequest == null)
            {
                return BadRequest("HospitalizationRequest is null");
            }

            _hospitalizationService.UpdateHospitalization(HospitalizationUpdateRequest);

            return Ok("Updated successfully");
        } 
        public IActionResult RemoveHospitalization(int id)
        {
            if (_hospitalizationService.RemoveHospitalization(id))
            {
                return Ok("Removed successfully");
            }
            return BadRequest("Hospitalization not found");
        }



        [HttpGet("{id}")]
        public ActionResult<Hospitalization> GetHospitalizationById(int id)
        {
            var Hospitalization = _hospitalizationService.GetHospitalizationById(id);
            if (Hospitalization == null)
            {
                return BadRequest("Hospitalization not found");
            }
            return Ok(Hospitalization);
        }
    }
}
