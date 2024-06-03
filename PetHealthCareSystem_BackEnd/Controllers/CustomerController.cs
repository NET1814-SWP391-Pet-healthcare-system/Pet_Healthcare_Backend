using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Extensions;
using ServiceContracts;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Authorize(Policy = "CustomerPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public CustomerController(IUserService userService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet("pets")]
        public async Task<IActionResult> GetCustomerPetList() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerUsername = User.GetUsername();
            var customerModel = await _userManager.FindByNameAsync(customerUsername);
            if (customerModel == null) 
            { 
                return Unauthorized();
            }
            var petList = await _userService.GetCustomerWithPets(customerModel.Id);
            if (petList == null)
            {
                return NoContent();
            }
            return Ok(petList);
        }
    }
}
