using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
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

        [HttpPost]
        public IActionResult AddUser(UserAddRequest? userAddRequest)
        {
            if(userAddRequest == null)
            {
                return BadRequest("UserRequest is null");
            }

            _userService.AddUser(userAddRequest);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(user);
        }
    }
}
