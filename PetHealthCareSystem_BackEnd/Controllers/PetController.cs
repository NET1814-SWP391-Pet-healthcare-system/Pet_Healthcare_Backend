using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PetHealthCareSystem_BackEnd.Extensions;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
using Services;
using System.Security.Claims;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;

        public PetController(IPetService petService, UserManager<User> userManager, IPhotoService photoService)
        {
            _petService = petService;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPets()
        {
            return Ok(await _petService.GetAllPets());
        }

        [HttpGet("user-pet/{username}")]
        public async Task<IActionResult> GetPetsByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return NotFound("Username not found");
            }
            var petList = await _petService.GetAllPets();
            var usersPet = petList.Where(p => p.CustomerId.Equals(user.Id));
            if(!usersPet.Any())
            {
                return Ok("User doesn't have any pets");
            }
            return Ok(usersPet);
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddPet(PetAddRequest? petAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingUser = await _userManager.FindByNameAsync(petAddRequest.CustomerUsername);
            if(existingUser == null)
            {
                return NotFound("No customer found");
            }
            Pet pet = petAddRequest.ToPet();
            pet.CustomerId = existingUser.Id;
            var result = await _petService.AddPet(pet);
            return Ok(result);
        }

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPost("add-my-pet")]
        public async Task<IActionResult> AddMyPet(PetAddRequest? petAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            Pet pet = petAddRequest.ToPet();
            pet.CustomerId = _userManager.GetUserId(this.User);
            var result = await _petService.AddPet(pet);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var existingPet = await _petService.GetPetById(id);

            if(this.User.IsInRole("Customer"))
            {
                var customer = await _userManager.GetUserAsync(this.User);
                if(existingPet.CustomerId != customer.Id)
                {
                    return NotFound("You don't have this pet");
                }
            }

            if(existingPet == null)
            {
                return NotFound();
            }
            return Ok(existingPet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, PetUpdateRequest? petUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingPet = await _petService.GetPetById(id);
            {
                if(existingPet == null)
                {
                    return NotFound("PetId not found");
                }
            }
            Pet pet = petUpdateRequest.ToPet();
            pet.PetId = id;
            var result = await _petService.UpdatePet(pet);
            if(result == null)
            {
                return NotFound("Pet not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetById(int id)
        {
            var existingPet = await _petService.GetPetById(id);
            if(existingPet is null)
            {
                return NotFound("Pet not found");
            }
            if(this.User.IsInRole("Customer"))
            {
                var customer = await _userManager.GetUserAsync(this.User);
                if(existingPet.CustomerId != customer.Id)
                {
                    return NotFound("You don't have this pet");
                }
            }
            var isDeleted = await _petService.RemovePetById(id);
            if(!isDeleted)
            {
                return BadRequest("Pet deletion failed");
            }
            return Ok(existingPet);
        }

        [HttpPost("upload-pet-image/{petId}")]
        public async Task<IActionResult> UploadProfileImage(IFormFile imageFile, int petId)
        {
            var existingPet = await _petService.GetPetById(petId);
            if(existingPet is null)
            {
                return NotFound("Pet not found");
            }
            if(this.User.IsInRole("Customer"))
            {
                var customer = await _userManager.GetUserAsync(this.User);
                if(existingPet.CustomerId != customer.Id)
                {
                    return NotFound("You don't have this pet");
                }
            }

            if(existingPet.ImageUrl.IsNullOrEmpty())
            {
                var imageResult = await _photoService.AddPhotoAsync(imageFile);
                Pet pet = new Pet()
                {
                    PetId = petId,
                    ImageURL = imageResult.Url.ToString()
                };
                var result = await _petService.UpdatePet(pet);
                return Ok(result);
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(existingPet.ImageUrl);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Could not delete photo, exception: {ex.Message}");
                }
                var imageResult = await _photoService.AddPhotoAsync(imageFile);
                Pet pet = new Pet
                {
                    PetId = petId,
                    ImageURL = imageResult.Url.ToString()
                };
                var result = await _petService.UpdatePet(pet);
                return Ok(result);
            }

        }

    }
}
