using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.PetVaccinationDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.VaccineDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetVaccinationController : ControllerBase
    {
        private readonly IPetVaccinationService _petVaccinationService;
        private readonly IPetService _petService;
        private readonly IVaccineService _vaccineService;

        public PetVaccinationController(IPetVaccinationService petVaccinationService, IPetService petService, IVaccineService vaccineService)
        {
            _petVaccinationService = petVaccinationService;
            _petService = petService;
            _vaccineService = vaccineService;
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetPetVaccinations() 
        {
            return Ok(await _petVaccinationService.GetAllPetVaccinations());
        }

        [Authorize]
        [HttpGet("{petId}/{vaccineId}")]
        public async Task<IActionResult> GetPetVaccinations(int petId, int vaccineId)
        {
            var pet = await _petService.GetPetById(petId);
            if(pet == null)
            {
                return NotFound("Pet not found");
            }
            var vaccine = await _vaccineService.GetVaccineById(vaccineId);
            if(vaccine == null)
            {
                return NotFound("Vaccine not found");
            }
            var petVaccination = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(petVaccination == null)
            {
                return BadRequest("Pet has not injected this vaccine");
            }
            return Ok(petVaccination);
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
            var petVaccination = await _petVaccinationService.AddPetVaccination(petVaccinationAddRequest);
            if(petVaccination == null)
            {
                return BadRequest("Pet add failed");
            }
            return Ok(petVaccination);
        }

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpDelete("{petId}/{vaccineId}")]
        public async Task<IActionResult> DeletePetVaccinationById(int petId, int vaccineId)
        {
            var petVaccinationData = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(petVaccinationData == null)
            {
                return NotFound("Pet vaccination not found");
            }

            var isDeleted = await _petVaccinationService.RemovePetVaccination(petId, vaccineId);
            if(!isDeleted)
            {
                return BadRequest("Pet vaccination deletion failed");
            }
            return Ok(petVaccinationData);
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

            if(petId != petVaccinationUpdateRequest.PetId)
            {
                return BadRequest("Mismatched PetId");
            }

            if(vaccineId != petVaccinationUpdateRequest.VaccineId)
            {
                return BadRequest("Mismatched VaccineId");
            }

            var result = await _petVaccinationService.UpdatePetVaccination(petId, vaccineId, petVaccinationUpdateRequest);
            if(result == null)
            {
                return NotFound("Pet vaccination not found");
            }
            return Ok(result);
        }

    }
}
