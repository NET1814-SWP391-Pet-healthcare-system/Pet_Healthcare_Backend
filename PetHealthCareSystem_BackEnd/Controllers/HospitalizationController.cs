using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts.Mappers;
using Microsoft.AspNetCore.Identity;
using PetHealthCareSystem_BackEnd.Extensions;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalizationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPetService _petService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IHospitalizationService _hospitalizationService;

        public HospitalizationController(ApplicationDbContext context, IHospitalizationService hospitalizationService, IUserService userService
            , UserManager<User> userManager, IPetService petService)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _petService = petService;
            _hospitalizationService = hospitalizationService;
        }

        //Create
        [Authorize(Policy = "EmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddHospitalization([FromBody] HospitalizationAddRequest hospitalizationAddRequest)
        {
            var username = User.GetUsername();
            var userModel = await _userManager.FindByNameAsync(username);

            if (User.IsInRole("Customer"))
            {
                return BadRequest("Employee only");
            }
            if (hospitalizationAddRequest == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime admissionDate = Convert.ToDateTime(hospitalizationAddRequest.AdmissionDate);
            DateTime dischargeDate = Convert.ToDateTime(hospitalizationAddRequest.DischargeDate);

            if (admissionDate > dischargeDate)
            {
                return BadRequest("Admission Date cannot be greater than Discharge Date");
            }

            if (admissionDate == dischargeDate)
            {
                return BadRequest("Admission Date cannot be equal to Discharge Date");
            }

            if (dischargeDate < admissionDate)
            {
                return BadRequest("Discharge Date cannot be less than Admission Date");
            }

            if (hospitalizationAddRequest.TotalCost < 0)
            {
                return BadRequest("Total Cost cannot be negative");
            }
            var pet = await _petService.GetPetById(hospitalizationAddRequest.PetId);
            if (pet == null)
            {
                return BadRequest("This pet is not exist");
            }
            userModel = await _userManager.FindByNameAsync(hospitalizationAddRequest.VetId);
            if (userModel == null)
            {
                return BadRequest("Vet does not exist");
            }
            var kennel = await _context.Kennels.FindAsync(hospitalizationAddRequest.KennelId);
            if (kennel == null)
            {
                return BadRequest("Kennel does not exist");
            }
            hospitalizationAddRequest.VetId = userModel.Id;

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
