using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, 
            IUserService userService, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        ////Create
        //[HttpPost]
        //public ActionResult<BusinessResult> AddUser(UserAddRequest? userAddRequest)
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    if(!ModelState.IsValid)
        //    {
        //        businessResult.Status = 400;
        //        businessResult.Data = false;
        //        businessResult.Message = "Invalid request";
        //        return BadRequest(businessResult);
        //    }

        //    _userService.AddUser(userAddRequest);
        //    businessResult.Status = 200;
        //    businessResult.Data = userAddRequest;
        //    businessResult.Message = "User added";
        //    return Ok(businessResult);
        //}


        ////Read
        //[HttpGet]
        //public ActionResult<BusinessResult> GetUsers()
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    businessResult.Data = _userService.GetUsers(); ;
        //    businessResult.Message = "Successful";
        //    businessResult.Status = 200;
        //    return Ok(businessResult);
        //}

        //[HttpGet("customers")]
        //public ActionResult<BusinessResult> GetCustomers()
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    businessResult.Data = _userService.GetCustomers();
        //    businessResult.Message = "Successful";
        //    businessResult.Status = 200;
        //    return Ok(businessResult);
        //}

        //[HttpGet("vets")]
        //public ActionResult<BusinessResult> GetVets()
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    businessResult.Data = _userService.GetVets();
        //    businessResult.Message = "Successful";
        //    businessResult.Status = 200;
        //    return Ok(businessResult);
        //}

        //[HttpGet("id/{id}")]
        //public ActionResult<BusinessResult> GetUserById(int id)
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    var user = _userService.GetUserById(id);
        //    if(user == null)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "No User found";
        //        return NotFound(businessResult);
        //    }
        //    businessResult.Status = 200;
        //    businessResult.Data = user;
        //    businessResult.Message = "User found";
        //    return Ok(businessResult);
        //}

        ////Update
        //[HttpPut("{id}")]
        //public ActionResult<BusinessResult> UpdateUserById(int id, UserUpdateRequest? userUpdateRequest)
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    if(!ModelState.IsValid)
        //    {
        //        businessResult.Status = 400;
        //        businessResult.Data = false;
        //        businessResult.Message = "Request is null";
        //        return BadRequest(businessResult);
        //    }
        //    if(id != userUpdateRequest.UserId)
        //    {
        //        businessResult.Status = 400;
        //        businessResult.Data = false;
        //        businessResult.Message = "Mismatched id";
        //        return BadRequest(businessResult);
        //    }
        //    var result = _userService.UpdateUser(id, userUpdateRequest);
        //    if(result == null)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "User not found";
        //        return NotFound(businessResult);
        //    }
        //    businessResult.Status = 200;
        //    businessResult.Data = result;
        //    businessResult.Message = "User updated";
        //    return Ok(businessResult);
        //}

        ////Delete
        //[HttpDelete("{id}")]
        //public ActionResult<BusinessResult> DeleteUserById(int id)
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    var userData = _userService.GetUserById(id);
        //    if(userData == null)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = false;
        //        businessResult.Message = "User not found";
        //    }
        //    var isDeleted = _userService.RemoveUser(id);
        //    if(!isDeleted)
        //    {
        //        businessResult.Status = 400;
        //        businessResult.Data = false;
        //        businessResult.Message = "Didn't delete";
        //        return NotFound(businessResult);
        //    }
        //    businessResult.Status = 200;
        //    businessResult.Data = userData;
        //    businessResult.Message = "User deleted";
        //    return Ok(businessResult);
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            
            if (user == null) { return Unauthorized("Invalid username!"); }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) { return Unauthorized("Username not found or password incorrect"); }

            return Ok(
                new NewUserDto
                { 
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var customer = new Customer
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createCustomer = await _userManager.CreateAsync(customer, registerDto.Password);

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
                                Token = _tokenService.CreateToken(customer)
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
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
