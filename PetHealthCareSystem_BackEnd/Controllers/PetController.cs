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
        public async Task<ActionResult<BusinessResult>> GetPets()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Status = 200;
            businessResult.Message = "Get all pets succesfully";
            businessResult.Data = await _petService.GetAllPets();
            return Ok(businessResult);
        }

        [HttpGet("user-pet/{username}")]
        public async Task<ActionResult<IEnumerable<PetDTO>>> GetPetsByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("Username not found");
            }
            var petList = await _petService.GetAllPets();
            var usersPet = petList.Where(p => p.CustomerId.Equals(user.Id));
            if(!usersPet.Any())
            {
                return Ok("User doesn't have any pets");
            }
            return Ok(usersPet);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessResult>> AddPet(PetAddRequest? petAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Message = "Invalid request";
                businessResult.Data = false;
                return BadRequest(businessResult);
            }
            var existingUser = await _userManager.FindByNameAsync(petAddRequest.CustomerUsername);
            if (existingUser == null)
            {
                businessResult.Status = 400;
                businessResult.Message = "No Customer found";
                businessResult.Data = petAddRequest;
                return BadRequest(businessResult);
            }

            var data = await _petService.AddPet(petAddRequest);
            businessResult.Status = 200;
            businessResult.Message = "Pet added";
            businessResult.Data = data;
            return Ok(businessResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetDTO>> GetPetById(int id)
        {
            var pet = await _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PetDTO>> UpdatePet(int id, PetUpdateRequest? petUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != petUpdateRequest.PetId)
            {
                return BadRequest("Mismatched Id");
            }
            var result = await _petService.UpdatePet(id, petUpdateRequest);
            if (result == null)
            {
                return NotFound("Pet not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessResult>> DeletePetById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var petData = await _petService.GetPetById(id);
            if (petData == null)
            {
                businessResult.Status = 404;
                businessResult.Data = false;
                businessResult.Message = "Pet not found";
                return NotFound(businessResult);
            }
            var isDeleted = await _petService.RemovePetById(id);
            if (!isDeleted)
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
