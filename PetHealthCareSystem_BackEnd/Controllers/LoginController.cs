using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<BusinessResult> Login(LoginDTO request)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Message = "Login request is null";
                businessResult.Status = 400;
                businessResult.Data = false;
                return businessResult;
            }

            var user = _userService.GetUserByUsername(request.Username);
            if(user == null)
            {
                businessResult.Message = "Username or Password is wrong";
                businessResult.Status = 401;
                businessResult.Data = false;
                return businessResult;
            }
            if(!request.Password.Equals(user.Password))
            {
                businessResult.Message = "Username or Password is wrong";
                businessResult.Status = 401;
                businessResult.Data = false;
                return businessResult;
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Username", user.UserName.ToString()),
                new Claim("Email", user.Email.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
            );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            businessResult.Message = "Login successfully";
            businessResult.Status = 200;
            businessResult.Data = tokenValue;
            return businessResult;
        }
    }
}
