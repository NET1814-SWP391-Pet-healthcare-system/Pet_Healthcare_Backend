using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.RoleDTO;
using ServiceContracts;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase // Only one RoleController class
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoleService _roleService;

        public RoleController(ApplicationDbContext context, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        [HttpPost]
        public IActionResult AddRole(RoleAddRequest? roleAddRequest)
        {
            if (roleAddRequest == null)
            {
                return BadRequest("RoleRequest is null");
            }

            _roleService.AddRole(roleAddRequest);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetRoleById(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return BadRequest("Role not found");
            }
            return Ok(role);
        }
    }
}
