using CloudinaryDotNet.Actions;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpGet]
        public async Task<IActionResult> GetPets()
        {
            var petList = await _petService.GetAllPets();
            var result = petList.Select(pet => pet.ToPetDto());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("user-pet/{username}")]
        public async Task<IActionResult> GetPetsByUsername(string username)
        {
            if(this.User.IsInRole("Customer"))
            {
                var currentUsername = _userManager.GetUserName(this.User);
                if(currentUsername != username)
                {
                    return NotFound("Access Denied: You are not authorized to view or manage pets that do not belong to your account.");
                }
            }
            var user = await _userManager.FindByNameAsync(username);
            if(user == null)
            {
                return NotFound("Username not found");
            }

            var petList = await _petService.GetAllPets();
            var petListDto = petList.Select(pet => pet.ToPetDto());
            var usersPet = petListDto.Where(p => p.CustomerId.Equals(user.Id));
            if(!usersPet.Any())
            {
                return Ok("User doesn't have any pets");
            }
            return Ok(usersPet);
        }

        [Authorize(Policy = "CustomerOrEmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddPet(PetAddRequest? petAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            if(this.User.IsInRole("Customer"))
            {
                var customer = await _userManager.GetUserAsync(this.User);
                petAddRequest.CustomerUsername = customer.UserName;
            }

            var existingUser = await _userManager.FindByNameAsync(petAddRequest.CustomerUsername);
            if(existingUser == null)
            {
                return NotFound("No customer found");
            }
            Pet pet = petAddRequest.ToPet();
            pet.CustomerId = existingUser.Id;
            var result = await _petService.AddPet(pet);
            return Ok(result.ToPetDto());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var existingPet = await _petService.GetPetById(id);
            if(existingPet == null)
            {
                return NotFound();
            }
            if(this.User.IsInRole("Customer"))
            {
                var customer = await _userManager.GetUserAsync(this.User);
                if(existingPet.CustomerId != customer.Id)
                {
                    return NotFound("You don't have this pet");
                }
            }

            return Ok(existingPet.ToPetDto());
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, PetUpdateRequest? petUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingPet = await _petService.GetPetById(id);

            if(existingPet == null)
            {
                return NotFound("PetId not found");
            }
            if(this.User.IsInRole("Customer"))
            {
                var currentUserId = _userManager.GetUserId(this.User);
                if(currentUserId != existingPet.CustomerId)
                {
                    return NotFound("You don't have this pet");
                }
            }

            Pet pet = petUpdateRequest.ToPet();
            pet.PetId = id;

            if(petUpdateRequest.imageFile != null)
            {
                ImageUploadResult imageResult = new ImageUploadResult();
                if(existingPet.ImageURL.IsNullOrEmpty())
                {
                    imageResult = await _photoService.AddPhotoAsync(petUpdateRequest.imageFile);
                }
                else
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(existingPet.ImageURL);
                    }
                    catch(Exception ex)
                    {
                        return BadRequest($"Could not delete photo, exception: {ex.Message}");
                    }
                    imageResult = await _photoService.AddPhotoAsync(petUpdateRequest.imageFile);
                }

                pet.ImageURL = imageResult.Url.ToString();
            }
            
            var result = await _petService.UpdatePet(pet);
            if(result == null)
            {
                return NotFound("Pet not found");
            }
            return Ok(result.ToPetDto());
        }

        [Authorize]
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
            return Ok(existingPet.ToPetDto());
        }
    }
}
