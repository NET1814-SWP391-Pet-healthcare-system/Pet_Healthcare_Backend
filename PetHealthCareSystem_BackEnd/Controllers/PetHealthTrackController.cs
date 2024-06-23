using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.PetHealthTrackDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Printing;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PetHealthTrackController : ControllerBase
    {
        private readonly IPetHealthTrackService _petHealthTrackService;
        private readonly IPetService _petService;
        private readonly UserManager<User> _userManager;

        public PetHealthTrackController(IPetHealthTrackService petHealthTrackService, IPetService petService, UserManager<User> userManager)
        {
            _petHealthTrackService = petHealthTrackService;
            _petService = petService;
            _userManager = userManager;
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
        public async Task<IActionResult> GetPetHealthTrackById(int id)
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

        [HttpGet("user")]
        public async Task<IActionResult> GetPetHealthTrackByUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var pets = await _petService.GetAllPets();
            var id = _userManager.GetUserId(this.User);
            var user = await _userManager.FindByIdAsync(id);
            pets = pets.Where(x => x.CustomerId == user.Id).ToList();

            var petHealthTracks = await _petHealthTrackService.GetPetHealthTracksAsync();
            if (petHealthTracks == null)
            {
                return BadRequest("No pet health tracks");
            }
            var userPetHealthTracks = petHealthTracks.Where(healthTrack => pets.Any(pet => pet.PetId == healthTrack.Hospitalization.PetId)).ToList();

            return Ok(userPetHealthTracks.Select(x => x.ToPetHealthTrackDTO()));
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
