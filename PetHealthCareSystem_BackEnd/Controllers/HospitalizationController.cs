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
using Microsoft.EntityFrameworkCore;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IHospitalizationService _hospitalizationService;
        private readonly IPetService _petService;
        private readonly IKennelService _kennelService;



        public HospitalizationController(IHospitalizationService hospitalizationService, IUserService userService
            , UserManager<User> userManager, IPetService petService, IKennelService kennelService)
        {
            _userService = userService;
            _userManager = userManager;
            _hospitalizationService = hospitalizationService;
            _petService = petService;
            _kennelService = kennelService;
        }

        //Create
        // [Authorize(Policy = "EmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddHospitalization([FromBody] HospitalizationAddRequest hospitalizationAddRequest)
        {
            try
            {
                //var username = User.GetUsername();
                //var username = _userManager.GetUserName(this.User);
                //var userModel = await _userManager.FindByNameAsync(username);

                if (!ModelState.IsValid)
                {
                    string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return Problem(errorMessage);
                }
                DateOnly adDate = DateOnly.Parse(hospitalizationAddRequest.AdmissionDate);
                DateOnly disDate = DateOnly.Parse(hospitalizationAddRequest.DischargeDate); 
                var vet = await _userManager.FindByNameAsync(hospitalizationAddRequest.VetId);
                hospitalizationAddRequest.VetId = vet.Id;
                bool isVetFree = await _hospitalizationService.IsVetFree(hospitalizationAddRequest.VetId, adDate, disDate);
                if (isVetFree)
                {
                    return BadRequest("Vet is busy");
                }
                var hospi = hospitalizationAddRequest.ToHospitalizationFromAdd();
                string error =  HospitalizationValidation.HospitalizationVerification(hospitalizationAddRequest,_kennelService,_petService,_userManager,_userService);
                if (error != null)
                {
                    return BadRequest(error);
                }
                return Ok(await _hospitalizationService.AddHospitalization(hospi));
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }

        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetHospitalizations()
        {
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
            

                if (!ModelState.IsValid)
                {
                    string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return Problem(errorMessage);
                }
            var existingHospitalization = await _hospitalizationService.GetHospitalizationById(id);

                if (existingHospitalization == null)
                {
                    return NotFound("Hospitalization not found");
                }
                var hospi = hospitalizationUpdateRequest.ToHospitalizationUpdate();
            hospi.HospitalizationId = id;
            var isUpdated = await _hospitalizationService.UpdateHospitalization(hospi);
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(isUpdated);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalizationByHospitalizationByID(int id)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingHospitalization = await _hospitalizationService.GetHospitalizationById(id);
            if (existingHospitalization == null)
            {
                return NotFound("Hospitalization not found");
            }
            var isDeleted = await _hospitalizationService.RemoveHospitalization(id);
            return Ok(existingHospitalization);
        }

        [HttpGet("VetName{id}")]
        public async Task<IActionResult> GetAllHospitalizationByVetID(string id)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vet = await _userManager.FindByNameAsync(id);
            id = vet.Id;
            var hospitalization = await _hospitalizationService.GetAllHospitalizationByVetId(id);
            if (hospitalization == null)
            {
                return NotFound();
            }
            return Ok(hospitalization);
        }

    }
}
