using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        //Create
        [HttpPost]
        public ActionResult<BusinessResult> AddUser(UserAddRequest? userAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Data = false;
                businessResult.Message = "Invalid request";
                return BadRequest(businessResult);
            }

            _userService.AddUser(userAddRequest);
            businessResult.Status = 200;
            businessResult.Data = userAddRequest;
            businessResult.Message = "User added";
            return Ok(businessResult);
        }


        //Read
        [HttpGet]
        public ActionResult<BusinessResult> GetUsers()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _userService.GetUsers(); ;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("customers")]
        public ActionResult<BusinessResult> GetCustomers()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _userService.GetCustomers();
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("vets")]
        public ActionResult<BusinessResult> GetVets()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _userService.GetVets();
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetUserById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No User found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = user;
            businessResult.Message = "User found";
            return Ok(businessResult);
        }

        //Update
        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdateUserById(int id, UserUpdateRequest? userUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if(!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Data = false;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            if(id != userUpdateRequest.UserId)
            {
                businessResult.Status = 400;
                businessResult.Data = false;
                businessResult.Message = "Mismatched id";
                return BadRequest(businessResult);
            }
            var result = _userService.UpdateUser(id, userUpdateRequest);
            if(result == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "User not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = result;
            businessResult.Message = "User updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<BusinessResult> DeleteUserById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var userData = _userService.GetUserById(id);
            if(userData == null)
            {
                businessResult.Status = 404;
                businessResult.Data = false;
                businessResult.Message = "User not found";
            }
            var isDeleted = _userService.RemoveUser(id);
            if(!isDeleted)
            {
                businessResult.Status = 400;
                businessResult.Data = false;
                businessResult.Message = "Didn't delete";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = userData;
            businessResult.Message = "User deleted";
            return Ok(businessResult);
        }
    }
}
