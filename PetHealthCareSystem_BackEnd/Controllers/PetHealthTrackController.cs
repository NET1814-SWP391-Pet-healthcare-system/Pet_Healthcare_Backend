using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.PetHealthTrackDTO;
using ServiceContracts;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetHealthTrackController : ControllerBase // Only one PetHealthTrackController class
    {
        private readonly ApplicationDbContext _context;
        private readonly IPetHealthTrackService _petHealthTrackService;

        public PetHealthTrackController(ApplicationDbContext context, IPetHealthTrackService petHealthTrackService)
        {
            _context = context;
            _petHealthTrackService = petHealthTrackService;
        }

        [HttpPost]
        public IActionResult AddPetHealthTrack(PetHealthTrackAddRequest? petHealthTrackAddRequest)
        {
            if (petHealthTrackAddRequest == null)
            {
                return BadRequest("petHealthTrackRequest is null");
            }

            _petHealthTrackService.AddPetHealthTrack(petHealthTrackAddRequest);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public ActionResult<PetHealthTrack> GetPetHealthTrackById(int id)
        {
            var petHealthTrack = _petHealthTrackService.GetPetHealthTrackById(id);
            if (petHealthTrack == null)
            {
                return BadRequest("PetHealthTrack not found");
            }
            return Ok(petHealthTrack);
        }
    }
}
