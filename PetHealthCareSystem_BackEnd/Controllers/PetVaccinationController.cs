using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.PetVaccinationDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.VaccineDTO;
using ServiceContracts.Mappers;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetVaccinationController : ControllerBase
    {
        private readonly IPetVaccinationService _petVaccinationService;
        private readonly IPetService _petService;
        private readonly IVaccineService _vaccineService;
        private readonly UserManager<User> _userManager;

        public PetVaccinationController(IPetVaccinationService petVaccinationService, IPetService petService, IVaccineService vaccineService, UserManager<User> userManager)
        {
            _petVaccinationService = petVaccinationService;
            _petService = petService;
            _vaccineService = vaccineService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPetVaccinations() 
        {
            var petVaccinations = await _petVaccinationService.GetAllPetVaccinations();
            var result = petVaccinations.Select(pv => pv.ToPetVaccinationDto());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{petId}/{vaccineId}")]
        public async Task<IActionResult> GetPetVaccinations(int petId, int vaccineId)
        {
            var existingPet = await _petService.GetPetById(petId);
            if(existingPet == null)
            {
                return NotFound("Pet not found");
            }
            if(this.User.IsInRole("Customer"))
            {
                var currentUserId = _userManager.GetUserId(this.User);
                if(currentUserId != existingPet.CustomerId)
                {
                    return NotFound("You don't have this pet");
                }
            }
            
            var vaccine = await _vaccineService.GetVaccineById(vaccineId);
            if(vaccine is null)
            {
                return NotFound("Vaccine not found");
            }

            var petVaccination = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(petVaccination is null)
            {
                return BadRequest("Pet has not injected this vaccine");
            }

            return Ok(petVaccination.ToPetVaccinationDto());
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var pet = await _petService.GetPetById(petVaccinationAddRequest.PetId);
            if(pet == null)
            {
                return NotFound("Pet not found");
            }

            var vaccine = await _vaccineService.GetVaccineById(petVaccinationAddRequest.VaccineId);

            if(vaccine == null)
            {
                return NotFound("Vaccine not found");
            }
            var result = await _petVaccinationService.AddPetVaccination(petVaccinationAddRequest.ToPetVaccination());
            if(result == null)
            {
                return BadRequest("Pet vaccination add failed");
            }
            return Ok(result.ToPetVaccinationDto());
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpDelete("{petId}/{vaccineId}")]
        public async Task<IActionResult> DeletePetVaccinationById(int petId, int vaccineId)
        {
            var existingPetVaccination = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(existingPetVaccination == null)
            {
                return NotFound("Pet vaccination not found");
            }

            var isDeleted = await _petVaccinationService.RemovePetVaccination(petId, vaccineId);
            if(!isDeleted)
            {
                return BadRequest("Pet vaccination deletion failed");
            }
            return Ok(existingPetVaccination.ToPetVaccinationDto());
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPut("{petId}/{vaccineId}")]
        public async Task<IActionResult> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var existingPetVaccination = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);

            if(existingPetVaccination is null)
            {
                return NotFound("Pet vaccination not found");
            }
            var petVaccination = petVaccinationUpdateRequest.ToPetVaccination();
            petVaccination.PetId = petId;
            petVaccination.VaccineId = vaccineId;

            var result = await _petVaccinationService.UpdatePetVaccination(petVaccination);
            if(result == null)
            {
                return NotFound("Pet vaccination not found");
            }
            return Ok(result.ToPetVaccinationDto());
        }

    }
}
