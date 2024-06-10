using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.Mappers;
using Microsoft.AspNetCore.Identity;
using PetHealthCareSystem_BackEnd.Extensions;
using Services;
using Microsoft.AspNetCore.Authorization;
using PetHealthCareSystem_BackEnd.Validations;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IHospitalizationService _hospitalizationService;
        private readonly HospitalizationValidation _hospitalizationValidation;



        public HospitalizationController(IHospitalizationService hospitalizationService, IUserService userService
            , UserManager<User> userManager, HospitalizationValidation hospitalizationValidation)
        {
            _userService = userService;
            _userManager = userManager;
            _hospitalizationService = hospitalizationService;
            _hospitalizationValidation = hospitalizationValidation;
        }

        //Create
        // [Authorize(Policy = "EmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddHospitalization([FromBody] HospitalizationAddRequest hospitalizationAddRequest)
        {
            //var username = User.GetUsername();
            var username = _userManager.GetUserName(this.User);
            var userModel = await _userManager.FindByNameAsync(username);

            if (User.IsInRole("Customer"))
            {
                return BadRequest("Employee only");
            }
            if (hospitalizationAddRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string error = await _hospitalizationValidation.HospitalizationVerificaiton(hospitalizationAddRequest);
            if (error != null)
            {
                return BadRequest(error);
            }
            var vet = await _userManager.FindByNameAsync(hospitalizationAddRequest.VetId);
            hospitalizationAddRequest.VetId = vet.Id;
            await _hospitalizationService.AddHospitalization(hospitalizationAddRequest.ToHospitalizationFromAdd());
            return Ok("Added Hospitalization Successfully");

        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetHospitalizations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hospitalizations = await _hospitalizationService.GetHospitalizations();
            var hospitalizationDtos = hospitalizations.Select(x => x.ToHospitalizationDto());
            return Ok(hospitalizationDtos);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetHospitalizationById(int id)
        {
            var hospitalization = await _hospitalizationService.GetHospitalizationById(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (hospitalization == null)
            {
                return NotFound();
            }
            return Ok(hospitalization);
        }


        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHospitalization(int id, [FromBody] HospitalizationUpdateRequest hospitalizationUpdateRequest)
        {
            var existingHospitalization = await _hospitalizationService.GetHospitalizationById(id);
            if (hospitalizationUpdateRequest == null || !ModelState.IsValid || existingHospitalization == null)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _hospitalizationService.UpdateHospitalization(id, hospitalizationUpdateRequest.ToHospitalizationUpdate());
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Updated Successfully");
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalizationByHospitalizationByID(int id)
        {
            var isDeleted = await _hospitalizationService.RemoveHospitalization(id);
            if (!ModelState.IsValid || isDeleted == null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Nuke successfully");
        }
    }
}
