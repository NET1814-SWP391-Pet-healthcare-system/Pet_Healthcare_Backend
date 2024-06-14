using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetHealthCareSystem_BackEnd.Extensions;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
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

        public PetController(IPetService petService, UserManager<User> userManager)
        {
            _petService = petService;
            _userManager = userManager;
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

            var pet = await _petService.AddPet(petAddRequest);
            return Ok(pet);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _petService.GetPetById(id);
            if(pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, PetUpdateRequest? petUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                if(!ModelState.IsValid)
                {
                    string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return Problem(errorMessage);
                }
            }
            if(id != petUpdateRequest.PetId)
            {
                return BadRequest("Mismatched Id");
            }
            var result = await _petService.UpdatePet(id, petUpdateRequest);
            if(result == null)
            {
                return NotFound("Pet not found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetById(int id)
        {
            var petData = await _petService.GetPetById(id);
            if(petData == null)
            {
                return NotFound("Pet not found");
            }
            var isDeleted = await _petService.RemovePetById(id);
            if(!isDeleted)
            {
                return BadRequest("Pet deletion failed");
            }
            return Ok(petData);
        }
    }
}
