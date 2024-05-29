using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("users/{username}")]
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
            if (usersContainsUsername == null)
            {
                return NoContent();
            }
            return Ok(usersContainsUsername);
        }

        [HttpPost("register-user")]
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
                                return Ok(
                                    new NewUserDto
                                    {
                                        UserName = customer.UserName,
                                        Email = customer.Email,
                                        Token = _tokenService.CreateToken(customer, "Customer")
                                    }
                                );
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
                                return Ok(
                                    new NewUserDto
                                    {
                                        UserName = vet.UserName,
                                        Email = vet.Email,
                                        Token = _tokenService.CreateToken(vet, "Vet")
                                    }
                                );
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
                                return Ok(
                                    new NewUserDto
                                    {
                                        UserName = employee.UserName,
                                        Email = employee.Email,
                                        Token = _tokenService.CreateToken(employee, "Employee")
                                    }
                                );
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

        [HttpPut("update-user/{username}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string username ,[FromBody] UserUpdateRequest userUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound();
                }
                user.UserName = (userUpdateDto.Username != null) ? userUpdateDto.Username : user.UserName;
                user.Email = (userUpdateDto.Email != null) ? userUpdateDto.Email : user.Email;
                user.FirstName = (userUpdateDto.FirstName != null) ? userUpdateDto.FirstName : user.FirstName;
                user.LastName = (userUpdateDto.LastName != null) ? userUpdateDto.LastName : user.LastName;
                user.Gender = (userUpdateDto.Gender != null) ? userUpdateDto.Gender : user.Gender;
                user.Address = (userUpdateDto.Address != null) ? userUpdateDto.Address : user.Address;
                user.Country = (userUpdateDto.Country != null) ? userUpdateDto.Country : user.Country;
                user.ImageURL = (userUpdateDto.ImageURL != null) ? userUpdateDto.ImageURL : user.ImageURL;
                user.IsActive = (userUpdateDto.IsActive != null) ? userUpdateDto.IsActive : user.IsActive;
                if (user is Vet)
                {
                    Vet vet = (Vet)user;
                    vet.Rating = (userUpdateDto.Rating != null) ? userUpdateDto.Rating : vet.Rating;
                    vet.YearsOfExperience = (userUpdateDto.YearsOfExperience != null) ? userUpdateDto.YearsOfExperience : vet.YearsOfExperience;
                    await _userManager.UpdateAsync(vet);
                    await _userManager.UpdateAsync(user);
                    return Ok(vet);
                }
                else if (user is Customer)
                {
                    Customer customer = (Customer)user;
                    await _userManager.UpdateAsync(customer);
                    await _userManager.UpdateAsync(user);
                    return Ok(customer);
                }
                else
                {
                    await _userManager.UpdateAsync(user);
                    return Ok(user);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("delete-user/{username}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) 
            { 
                return NotFound();
            }
            var userModel = await _userManager.DeleteAsync(user);
            return Ok(userModel);
        }
    }
}
