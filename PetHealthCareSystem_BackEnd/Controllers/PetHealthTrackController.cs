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
 //   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PetHealthTrackController : ControllerBase
    {
        private readonly IPetHealthTrackService _petHealthTrackService;
        private readonly IPetService _petService;
        private readonly UserManager<User> _userManager;
        private readonly IHospitalizationService _hospitalizationService;

        public PetHealthTrackController(IPetHealthTrackService petHealthTrackService, IPetService petService, UserManager<User> userManager, IHospitalizationService hospitalizationService)
        {
            _petHealthTrackService = petHealthTrackService;
            _petService = petService;
            _userManager = userManager;
            _hospitalizationService = hospitalizationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPetHealthTrack(PetHealthTrackAddRequest? petHealthTrack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("petHealthTrackRequest is null");
            }
            var peth = petHealthTrack.ToPetHealthTrackFromAdd();
            var hospi = await _hospitalizationService.GetHospitalizationById((int)peth.HospitalizationId);
            if(hospi == null)
            {
                return BadRequest("Hospitalization not found");
            }
            peth.Hospitalization = hospi;
            var result = await _petHealthTrackService.AddPetHealthTrackAsync(peth);

            return Ok(result.ToPetHealthTrackDTO());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetHealthTrackById(int id)
        {
            var petHealthTrack = await _petHealthTrackService.GetPetHealthTrackByIdAsync(id);
            if (petHealthTrack == null)
            {
                return BadRequest("PetHealthTrack not found");
            }
            return Ok(petHealthTrack.ToPetHealthTrackDTO());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPetHealthTrack()
        {
            var healtht = await _petHealthTrackService.GetPetHealthTracksAsync();
            var healthtDtos = 
                healtht.Select(x => x.ToPetHealthTrackDTO());
            return Ok(healthtDtos);
        }

        [HttpGet("hospitalization/{id}")]
        public async Task<IActionResult> GetPetHealthTracksByHospitalizationId(int id)
        {
            var petHealthTracks = await _petHealthTrackService.GetPetHealthTracksAsync();
            var healthtDtos = petHealthTracks.Select(x => x.ToPetHealthTrackDTO());
            if (healthtDtos == null)
            {
                return BadRequest("No pet health tracks found for the provided Hospitalization ID");
            }

            // Filter pet health tracks by pet ID
            var petHealthTracksForPetId = healthtDtos.Where(track => track.HospitalizationId == id);

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
            pets = pets.Where(x => x.CustomerId == user.Id);

            var petHealthTracks = await _petHealthTrackService.GetPetHealthTracksAsync();
            if (petHealthTracks == null)
            {
                return BadRequest("No pet health tracks");
            }
            return Ok(petHealthTracks.Select(x => x.ToPetHealthTrackDTO()));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePetHealthTrack(int id, PetHealthTrackUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            var existingPetHealthTrack = await _petHealthTrackService.GetPetHealthTrackByIdAsync(id);
            if (existingPetHealthTrack == null)
            {
                return NotFound("PetHealthTrack not found");
            }
            var petHealthTrack = request.ToPetHealthTrackFromUpdate();
            petHealthTrack.PetHealthTrackId = id;
            var updatedPetHealthTrack = await _petHealthTrackService.UpdatePetHealthTrackAsync(petHealthTrack);
            if (updatedPetHealthTrack == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(updatedPetHealthTrack.ToPetHealthTrackDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePetHealthTrack(int id)
        {
            var removedPetHealthTrack = await _petHealthTrackService.GetPetHealthTrackByIdAsync(id);
            if (removedPetHealthTrack == null)
            {
                return NotFound("PetHealthTrack not found");
            }
            var isDeleted = await _petHealthTrackService.RemovePetHealthTrackAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Delete Fail");
            }
            return Ok(removedPetHealthTrack.ToPetHealthTrackDTO());
        }
    }

   }
