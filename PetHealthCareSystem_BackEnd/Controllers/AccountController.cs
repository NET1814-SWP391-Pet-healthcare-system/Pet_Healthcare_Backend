//using Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using ServiceContracts;
//using ServiceContracts.DTO.Result;
//using ServiceContracts.DTO.UserDTO;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace PetHealthCareSystem_BackEnd.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly IUserService _userService;
//        private readonly IConfiguration _configuration;

//        public LoginController(IUserService userService, IConfiguration configuration)
//        {
//            _userService = userService;
//            _configuration = configuration;
//        }

//        [HttpPost]
//        public ActionResult<BusinessResult> Login(LoginDTO request)
//        {
//            BusinessResult businessResult = new BusinessResult();
//            if(!ModelState.IsValid)
//            {
//                businessResult.Message = "Login request is null";
//                businessResult.Status = 400;
//                businessResult.Data = false;
//                return businessResult;
//            }

//            var user = _userService.GetUserById(request.Username);
//            if(user == null)
//            {
//                businessResult.Message = "Username or Password is wrong";
//                businessResult.Status = 401;
//                businessResult.Data = false;
//                return businessResult;
//            }
//            if(!request.Password.Equals(user.Password))
//            {
//                businessResult.Message = "Username or Password is wrong";
//                businessResult.Status = 401;
//                businessResult.Data = false;
//                return businessResult;
//            }
//            businessResult.Message = "Login successfully";
//            businessResult.Status = 200;
//            businessResult.Data = user;
//            return businessResult;
//        }
//    }
//}
