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

        [HttpGet]
        public async Task<ActionResult<BusinessResult>> GetPetVaccinations()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Status = 200;
            businessResult.Message = "Get all pet vaccinations successfully";
            businessResult.Data = await _petVaccinationService.GetAllPetVaccinations();
            return Ok(businessResult);
        }

        [HttpGet("{petId}/{vaccineId}")]
        public async Task<ActionResult<BusinessResult>> GetPetVaccinations(int petId, int vaccineId)
        {
            BusinessResult businessResult = new BusinessResult();
            var pet = _petService.GetPetById(petId);
            if(pet == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Pet not found";
                businessResult.Data = false;
                return NotFound(businessResult);
            }
            var vaccine = await _vaccineService.GetVaccineById(vaccineId);
            if(vaccine == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Vaccine not found";
                businessResult.Data = false;
                return NotFound(businessResult);
            }
            var petVaccination = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(petVaccination == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Pet has not injected this vaccine";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Get pet vaccination successfully";
            businessResult.Data = petVaccination;
            return Ok(businessResult);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessResult>> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var pet = _petService.GetPetById(petVaccinationAddRequest.PetId);
            if(pet == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Pet not found";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }

            var vaccine = await _vaccineService.GetVaccineById(petVaccinationAddRequest.VaccineId);

            if(vaccine == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Vaccine not found";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var petVaccination = await _petVaccinationService.AddPetVaccination(petVaccinationAddRequest);
            if(petVaccination == null)
            {
                businessResult.Status = 400;
                businessResult.Message = "Didn't add";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Pet vaccination added";
            businessResult.Data = petVaccination;
            return Ok(businessResult);
        }

        [HttpDelete("{petId}/{vaccineId}")]
        public async Task<ActionResult<BusinessResult>> DeletePetVaccinationById(int petId, int vaccineId)
        {
            BusinessResult businessResult = new BusinessResult();
            var petVaccinationData = await _petVaccinationService.GetPetVaccinationById(petId, vaccineId);
            if(petVaccinationData == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Pet vaccination not found";
                businessResult.Data = false;
                return NotFound(businessResult);
            }

            var isDeleted = await _petVaccinationService.RemovePetVaccination(petId, vaccineId);
            if(!isDeleted)
            {
                businessResult.Status = 400;
                businessResult.Message = "Didn't delete";
                businessResult.Data = petVaccinationData;
                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Pet vaccination deleted";
            businessResult.Data = petVaccinationData;
            return Ok(businessResult);
        }

        [HttpPut("{petId}/{vaccineId}")]
        public async Task<ActionResult<BusinessResult>> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }

            if(petId != petVaccinationUpdateRequest.PetId)
            {
                businessResult.Status = 400;
                businessResult.Message = "Mismatched PetId";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }

            if(vaccineId != petVaccinationUpdateRequest.VaccineId)
            {
                businessResult.Status = 400;
                businessResult.Message = "Mismatched VaccineId";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }

            var result = await _petVaccinationService.UpdatePetVaccination(petId, vaccineId, petVaccinationUpdateRequest);
            if(result == null)
            {
                businessResult.Status = 404;
                businessResult.Message = "Pet vaccination not found";
                businessResult.Data = petVaccinationUpdateRequest;
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Message = "Pet vaccination updated";
            businessResult.Data = result;
            return Ok(businessResult);
        }

    }
}
