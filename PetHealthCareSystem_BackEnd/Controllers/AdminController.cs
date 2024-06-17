using Entities;
using Entities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetHealthCareSystem_BackEnd.Extensions;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
using System.Text.RegularExpressions;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        public AdminController(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserDTO>();
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                var userDto = user.ToUserDtoFromUser();
                userDto.Role = role.SingleOrDefault();
                result.Add(userDto);
            }
            if (users == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUsersByUsername([FromRoute] string username)
        {
            var users = await _userManager.Users.ToListAsync();
            var usersContainsUsername = new List<User>();
            foreach (var user in users)
            {
                if (user.UserName.Contains(username))
                {
                    usersContainsUsername.Add(user);
                }
            }
            if (usersContainsUsername.IsNullOrEmpty())
            {
                return NoContent();
            }
            return Ok(usersContainsUsername);
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpPut("update-profile/{userId}")]
        public async Task<IActionResult> UpdateProfile(string userId, UserUpdateRequest userUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(userUpdateRequest.Username != null)
            {   
                //check if the requested username, if not the same as old username and is used by an another user
                if(await _userManager.FindByNameAsync(userUpdateRequest?.Username) != null &&
                    !userUpdateRequest.Username.Equals(user.UserName))
                {
                    return Conflict("The requested username is already in use. Please choose a different username.");
                }
            }
            if(!string.IsNullOrEmpty(userUpdateRequest.PhoneNumber))
            {
                string pattern = @"^(0\d{9,10})$";
                Regex regex = new Regex(pattern);

                if(!regex.IsMatch(userUpdateRequest.PhoneNumber))
                {
                    return BadRequest("Phone number is in wrong format");
                }
            }

            var result = await _userManager.UpdateUserAsync(userId, userUpdateRequest);
            if(result == null)
            {
                return NotFound("UserId not found");
            }
            return Ok(result);
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpGet("users/role/{role}")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            switch(role.ToUpper())
            {
                case "CUSTOMER":
                    var customerList = await _userManager.GetUserDtosInRoleAsync("Customer");
                    return Ok(customerList);

                case "VET":
                    var vetList = await _userManager.GetUserDtosInRoleAsync("Vet");
                    return Ok(vetList);

                case "EMPLOYEE":
                    var employeeList = await _userManager.GetUserDtosInRoleAsync("Employee");
                    return Ok(employeeList);

                default:
                    return BadRequest("Role does not exist");
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserAddRequest userAddDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                switch (userAddDto.Role.ToLower())
                {
                    case "customer":
                        var customer = new Customer
                        {
                            UserName = userAddDto.Username,
                            Email = userAddDto.Email,
                            FirstName = userAddDto.FirstName,
                            LastName = userAddDto.LastName,
                            Gender = userAddDto.Gender,
                            Address = userAddDto.Address,
                            Country = userAddDto.Country,
                            ImageURL = userAddDto.ImageURL,
                            IsActive = userAddDto.IsActice
                        };

                        var createCustomer = await _userManager.CreateAsync(customer, userAddDto.Password);

                        if (createCustomer.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(customer, "Customer");
                            if (roleResult.Succeeded)
                            {
                                return Ok(_tokenService.CreateToken(customer, "Customer"));
                            }
                            
                            else
                            {
                                return StatusCode(500, roleResult.Errors);
                            }
                        }
                        else
                        {
                            return StatusCode(500, createCustomer.Errors);
                        }
                    case "vet":
                        var vet = new Vet
                        {
                            UserName = userAddDto.Username,
                            Email = userAddDto.Email,
                            Rating = userAddDto.Rating,
                            YearsOfExperience = userAddDto.YearsOfExperience,
                            FirstName = userAddDto.FirstName,
                            LastName = userAddDto.LastName,
                            Gender = userAddDto.Gender,
                            Address = userAddDto.Address,
                            Country = userAddDto.Country,
                            ImageURL = userAddDto.ImageURL,
                            IsActive = userAddDto.IsActice
                        };

                        var createVet = await _userManager.CreateAsync(vet, userAddDto.Password);

                        if (createVet.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(vet, "Vet");
                            if (roleResult.Succeeded)
                            {
                                return Ok(_tokenService.CreateToken(vet, "Vet"));
                            }
                            else
                            {
                                return StatusCode(500, roleResult.Errors);
                            }
                        }
                        else
                        {
                            return StatusCode(500, createVet.Errors);
                        }
                    case "employee":
                        var employee = new User
                        {
                            UserName = userAddDto.Username,
                            Email = userAddDto.Email,
                            FirstName = userAddDto.FirstName,
                            LastName = userAddDto.LastName,
                            Gender = userAddDto.Gender,
                            Address = userAddDto.Address,
                            Country = userAddDto.Country,
                            ImageURL = userAddDto.ImageURL,
                            IsActive = userAddDto.IsActice
                        };

                        var createEmployee = await _userManager.CreateAsync(employee, userAddDto.Password);

                        if (createEmployee.Succeeded)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(employee, "Employee");
                            if (roleResult.Succeeded)
                            {
                                return Ok(_tokenService.CreateToken(employee, "Employee"));
                            }
                            else
                            {
                                return StatusCode(500, roleResult.Errors);
                            }
                        }
                        else
                        {
                            return StatusCode(500, createEmployee.Errors);
                        }
                    default:
                        return BadRequest($"{userAddDto.Role} is not a suitable role");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
            { 
                return NotFound();
            }
            var userModel = await _userManager.DeleteAsync(user);
            return Ok(userModel);
        }
    }
}
