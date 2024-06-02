using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.PetHealthTrackDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;

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
        public async Task<IActionResult> AddPetHealthTrack(PetHealthTrack? petHealthTrack)
        {
            BusinessResult businessResult = new BusinessResult();
            if (petHealthTrack == null)
            {
                return BadRequest("petHealthTrackRequest is null");
            }

            _petHealthTrackService.AddPetHealthTrackAsync(petHealthTrack);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetHealthTrack>> GetPetHealthTrackById(int id)
        {
            var petHealthTrack = _petHealthTrackService.GetPetHealthTrackByIdAsync(id);
            if (petHealthTrack == null)
            {
                return BadRequest("PetHealthTrack not found");
            }
            return Ok(petHealthTrack);
        }
    }
}
