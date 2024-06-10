using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.PetHealthTrackDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;
using System.Threading.Tasks;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetHealthTrackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPetHealthTrackService _petHealthTrackService;

        public PetHealthTrackController(ApplicationDbContext context, IPetHealthTrackService petHealthTrackService)
        {
            _context = context;
            _petHealthTrackService = petHealthTrackService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPetHealthTrack(PetHealthTrackAddRequest? petHealthTrack)
        {
            BusinessResult businessResult = new BusinessResult();
            if (!ModelState.IsValid)
            {
                return BadRequest("petHealthTrackRequest is null");
            }

            await _petHealthTrackService.AddPetHealthTrackAsync(petHealthTrack);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetHealthTrack>> GetPetHealthTrackById(int id)
        {
            var petHealthTrack = await _petHealthTrackService.GetPetHealthTrackByIdAsync(id);
            if (petHealthTrack == null)
            {
                return BadRequest("PetHealthTrack not found");
            }
            return Ok(petHealthTrack);
        }
        [HttpGet]
        public async Task<ActionResult<PetHealthTrack>> GetAllPetHealthTrack()
        {
            var petHealthTrack = await _petHealthTrackService.GetPetHealthTracksAsync() ;
            if (petHealthTrack == null)
            {
                return BadRequest("PetHealthTrack not found");
            }
            petHealthTrack.ToList();
            return Ok(petHealthTrack);
        }
        [HttpGet("hospitalization/{id}")]
        public async Task<ActionResult<List<PetHealthTrack>>> GetPetHealthTracksByHospitalizationId(int id)
        {
            var petHealthTracks = await _petHealthTrackService.GetPetHealthTracksAsync();
            if (petHealthTracks == null)
            {
                return BadRequest("No pet health tracks found for the provided Hospitalization ID");
            }

            // Filter pet health tracks by pet ID
            var petHealthTracksForPetId = petHealthTracks.Where(track => track.HospitalizationId == id).ToList();

            return Ok(petHealthTracksForPetId);
        }



        [HttpPut]
        public async Task<IActionResult> UpdatePetHealthTrack(PetHealthTrackUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var updatedPetHealthTrack = await _petHealthTrackService.UpdatePetHealthTrackAsync(request);
            if (updatedPetHealthTrack == null)
            {
                return NotFound("PetHealthTrack not found");
            }

            return Ok(updatedPetHealthTrack);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePetHealthTrack(int id)
        {
            var removedPetHealthTrack = await _petHealthTrackService.RemovePetHealthTrackAsync(id);
            if (removedPetHealthTrack == null)
            {
                return NotFound("PetHealthTrack not found");
            }

            return Ok("Deleted successfully");
        }

    }
}
