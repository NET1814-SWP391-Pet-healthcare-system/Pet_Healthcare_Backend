using Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using PetHealthCareSystem_BackEnd.Extensions;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
using System.Security.Claims;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IPhotoService _photoService;

        public AccountController(UserManager<User> userManager,
            ITokenService tokenService, SignInManager<User> signInManager, IEmailService emailService, IPhotoService photoService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _photoService = photoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if(user == null)
            { return Unauthorized("Username not found or password incorrect"); }

            //Check if account is banned
            if(!user.IsActive ?? true)
            {
                return Forbid("Account is banned");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
            { return Unauthorized("Username not found or password incorrect"); }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            var authenticationResponse = _tokenService.CreateToken(user, role);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiryDate = authenticationResponse.RefreshTokenExpiryDate;
            //update refresh token and its expiry date
            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = new Customer
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Gender = registerDto.Gender,
                    Address = registerDto.Address,
                    Country = registerDto.Country,
                    ImageURL = registerDto.ImageURL,
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    IsActive = true
                };

                var createCustomer = await _userManager.CreateAsync(customer, registerDto.Password!);

                if(createCustomer.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(customer, "Customer");
                    if(roleResult.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(customer);
                        var role = roles.FirstOrDefault();

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(customer);
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = customer.Email }, Request.Scheme);
                        var message = new Message(new string[] { customer.Email! }, "Confirmation Email Link", confirmationLink!);

                        await _emailService.SendEmailAsync(message);
                        
                        return Ok(new NewUserDto
                        {
                            UserName = customer.UserName,
                            Email = customer.Email,
                            Role = role
                        });
;
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
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email!);
            if(user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var reactUrl = "http://localhost:5173/reset-password";
                //var forgotPasswordLink = Url.ActionLink(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
                var queryString = $"?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email!)}";
                var forgotPasswordLink = $"{reactUrl}{queryString}";
                var message = new Message(new string[] { user.Email! }, "Forgot Password Link", forgotPasswordLink!);
                await _emailService.SendEmailAsync(message);

                return Ok($"Password change request is sent on Email {user.Email}. Please open your email & click the link");
            }

            return BadRequest("Couldn't send link, please try again");
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var resetPasswordModel = new ResetPasswordRequest { Token = token, Email = email };
            return Ok(resetPasswordModel);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email!);
            if(user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPasswordRequest.Token!, resetPasswordRequest.Password!);
                if(!resetPasswordResult.Succeeded)
                {
                    foreach(var error in resetPasswordResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return Ok("Password has been changed");
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            var resetLink = "http://localhost:5173/";
            var message = new Message(new string[] { "soybean26102004@gmail.com" }, "Test", $"Reset your password using this link: <a href='{resetLink}'>link</a>");

            await _emailService.SendEmailAsync(message);
            return Ok("Email Sent Successfully");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    return Ok("Email verified successfully");
                }
            }
            return StatusCode(500, "This user does not exist");
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserUpdateRequest userUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            if(await _userManager.FindByEmailAsync(userUpdateRequest?.Email) != null)
            {
                return Conflict("The requested email is already in use. Please choose a different email.");
            }
            if(await _userManager.FindByNameAsync(userUpdateRequest?.Username) != null)
            {
                return Conflict("The requested username is already in use. Please choose a different username.");
            }

            var currentUserId = _userManager.GetUserId(this.User);
            var result = await _userManager.UpdateUserAsync(currentUserId, userUpdateRequest);
            if(result == null)
            {
                return NotFound("UserId not found");
            }
            return Ok(result);
        }

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if(tokenModel == null)
            {
                return BadRequest("Invalid client request");
            }

            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromJwtToken(tokenModel.Token);

            if(principal == null)
            {
                return BadRequest("Invalid JWT access token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);
            string? role = principal.FindFirstValue(ClaimTypes.Role);
            User user = await _userManager.FindByEmailAsync(email);
            if(user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryDate <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }
            AuthenticationResponse response = _tokenService.CreateToken(user, role);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryDate = response.RefreshTokenExpiryDate;
            await _userManager.UpdateAsync(user);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var username = _userManager.GetUserName(this.User);

            if(username is null)
            {
                return Unauthorized("Unauthorized user");
            }

            var user = await _userManager.FindByNameAsync(username);

            if(user is null)
            {
                return Unauthorized("Unauthorized user");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryDate = null;
            await _userManager.UpdateAsync(user);

            return Ok("Refresh token is revoked");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> MyProfile()
        {
            var id = _userManager.GetUserId(this.User);
            var currentUser = await _userManager.FindByIdAsync(id);
            return Ok(currentUser.ToUserDtoFromUser());
        }

        [Authorize]
        [HttpPost("upload-profile-image")]
        public async Task<IActionResult> UploadProfileImage(IFormFile imageFile)
        {
            var id = _userManager.GetUserId(this.User);
            var currentUser = await _userManager.FindByIdAsync(id);
            if (currentUser.ImageURL.IsNullOrEmpty())
            {
                var imageResult = await _photoService.AddPhotoAsync(imageFile);
                currentUser.ImageURL = imageResult.Url.ToString();
                _userManager.UpdateAsync(currentUser);
                return Ok(currentUser.ToUserDtoFromUser());
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(currentUser.ImageURL);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Could not delete photo, exception: {ex.Message}");
                }
                var imageResult = await _photoService.AddPhotoAsync(imageFile);
                currentUser.ImageURL = imageResult.Url.ToString();
                _userManager.UpdateAsync(currentUser);
                return Ok(currentUser.ToUserDtoFromUser());
            }

        }


        [HttpGet("run-seed-data-only-run-once")]
        public async Task<IActionResult> SeedUserRoles()
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var admin = await _userManager.FindByNameAsync("bjohnson");
                var customer1 = await _userManager.FindByNameAsync("jdoe");
                var customer2 = await _userManager.FindByNameAsync("jsmith");
                var vet1 = await _userManager.FindByNameAsync("ewilson");
                var vet2 = await _userManager.FindByNameAsync("mbrown");

                await _userManager.AddToRoleAsync(admin!, "Admin");
                await _userManager.AddToRoleAsync(customer1!, "Customer");
                await _userManager.AddToRoleAsync(customer2!, "Customer");
                await _userManager.AddToRoleAsync(vet1!, "Vet");
                await _userManager.AddToRoleAsync(vet2!, "Vet");
                return Ok("Role seeded");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }


    }
}
