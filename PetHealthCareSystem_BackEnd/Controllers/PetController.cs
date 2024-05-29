using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetHealthCareSystem_BackEnd.Extensions;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.Result;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly UserManager<User> _userManager;

        public PetController(IPetService petService, UserManager<User> userManager)
        {
            _petService = petService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<BusinessResult> GetPets()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Status = 200;
            businessResult.Message = "Get all pets succesfully";
            businessResult.Data = _petService.GetAllPets();
            return Ok(businessResult);
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var username = User.GetUsername();
            var currenUser = await _userManager.FindByNameAsync(username);
            return Ok(currenUser);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessResult>> AddPet(PetAddRequest? petAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var username = User.GetUsername();
            var existingUser = await _userManager.FindByNameAsync(username);
            if(existingUser == null)
            {
                businessResult.Status = 400;
                businessResult.Message = "No Customer found";
                businessResult.Data = petAddRequest;
                return BadRequest(businessResult);
            }
            if(!(existingUser is Customer))
            {
                businessResult.Status = 400;
                businessResult.Message = "User is not a customer";
                businessResult.Data = petAddRequest;
                return BadRequest(businessResult);
            }
            var data = _petService.AddPet(petAddRequest);
            businessResult.Status = 200;
            businessResult.Message = "Pet added";
            businessResult.Data = data;
            return Ok(businessResult);
        }

        [HttpGet("{id}")]
        public ActionResult<BusinessResult> GetPetById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var pet = _petService.GetPetById(id);
            if(pet == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Pet found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = pet;
            businessResult.Message = "Pet found";
            return Ok(businessResult);
        }

        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdatePet(int id, PetUpdateRequest? petUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            if(id != petUpdateRequest.PetId)
            {
                businessResult.Status = 400;
                businessResult.Message = "Mismatched id";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var existingUser = _userManager.FindByNameAsync("string");
            if(existingUser == null)
            {
                businessResult.Status = 400;
                businessResult.Message = "No Customer found";
                businessResult.Data = petUpdateRequest;
                return BadRequest(businessResult);
            }
            if(!(existingUser is Customer))
            {
                businessResult.Status = 400;
                businessResult.Message = "User is not a customer";
                businessResult.Data = petUpdateRequest;
                return BadRequest(businessResult);
            }

            var result = _petService.UpdatePet(id, petUpdateRequest);
            if(result == null)
            {
                businessResult.Status = 404;
                businessResult.Data = false;
                businessResult.Message = "Pet not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = result;
            businessResult.Message = "Pet updated";
            return Ok(businessResult);
        }

        [HttpDelete("{id}")]
        public ActionResult<BusinessResult> DeletePetById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var petData = _petService.GetPetById(id);
            if(petData == null)
            {
                businessResult.Status = 404;
                businessResult.Data = false;
                businessResult.Message = "Pet not found";
                return NotFound(businessResult);
            }
            var isDeleted = _petService.RemovePetById(id);
            if(!isDeleted)
            {
                businessResult.Status = 400;
                businessResult.Data = petData;
                businessResult.Message = "Didn't delete";

                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = petData;
            businessResult.Message = "Pet deleted";
            return Ok(businessResult);
        }
    }
}
