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
    //[Authorize]
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
                if (!ModelState.IsValid)
                {
                    string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return Problem(errorMessage);
                }
                DateOnly adDate = DateOnly.Parse(hospitalizationAddRequest.AdmissionDate);
                DateOnly disDate = DateOnly.Parse(hospitalizationAddRequest.DischargeDate); 

                bool isVetFree = await _hospitalizationService.IsVetFree(hospitalizationAddRequest.VetId, adDate, disDate);
                if (isVetFree)
                {
                    return BadRequest("Vet is busy");
                }


            var vet = await _userManager.FindByIdAsync(hospitalizationAddRequest.VetId);
            var kennel = await _kennelService.GetKennelById(hospitalizationAddRequest.KennelId);
            if (vet== null || await _userService.GetAvailableVetById(vet.Id) == null)
            {
                return NotFound("This vet does not exist");
            }
            hospitalizationAddRequest.VetId = vet.Id;
            if (adDate > disDate)
            {
                return BadRequest("Start date cannot be greater than end date");
            }

            var pet = await _petService.GetPetById(hospitalizationAddRequest.PetId);
            if (pet == null)
            {
                return BadRequest("This pet does not exist");
            }

            if (kennel == null)
            {
                return BadRequest("This kennel does not exist");
            }
            var hospi = hospitalizationAddRequest.ToHospitalizationFromAdd();
            hospi.Kennel = kennel;
            int numberOfDays = disDate.DayNumber - adDate.DayNumber + 1;
            hospi.TotalCost = numberOfDays * kennel.DailyCost;
            var result = await _hospitalizationService.AddHospitalization(hospi);
           return Ok(result.ToHospitalizationDto());
            

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
            if (hospitalization == null)
            {
                return NotFound();
            }
            return Ok(hospitalization.ToHospitalizationDto());
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
            if (hospi.DischargeDate < existingHospitalization.AdmissionDate)
            {
                return BadRequest("Discharge date cannot be lower than admission date");
            }
            hospi.Pet= await _petService.GetPetById((int)existingHospitalization.PetId);
            int numberOfDays = hospi.DischargeDate.Value.DayNumber - existingHospitalization.AdmissionDate.Value.DayNumber + 1;

            hospi.TotalCost = numberOfDays * existingHospitalization.Kennel.DailyCost;
            hospi.HospitalizationId = id;
            var isUpdated = await _hospitalizationService.UpdateHospitalization(hospi);
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(isUpdated.ToHospitalizationDto());
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospitalizationByHospitalizationID(int id)
        {
            var existingHospitalization = await _hospitalizationService.GetHospitalizationById(id);
            if (existingHospitalization == null)
            {
                return NotFound("Hospitalization not found");
            }
            var isDeleted = await _hospitalizationService.RemoveHospitalization(id);
            if (!isDeleted)
            {
                return BadRequest("Delete Fail");
            }
            return Ok(existingHospitalization.ToHospitalizationDto());
        }

        [HttpGet("VetName{id}")]
        public async Task<IActionResult> GetAllHospitalizationByVetID(string id)
        {
            var vet = await _userManager.FindByNameAsync(id);
            id = vet.Id;
            var hospitalizations = await _hospitalizationService.GetAllHospitalizationByVetId(id);
            var hospitalizationDtos = hospitalizations.Select(x => x.ToHospitalizationDto());
            return Ok(hospitalizationDtos);
        }
        [HttpGet("PetId/{id}")]
        public async Task<IActionResult> GetAllHospitalizationByPetId(int id)
        {
            var hospitalizations = await _hospitalizationService.GetAllHospitalizationByPetId(id);
            var hospitalizationDtos = hospitalizations.Select(x => x.ToHospitalizationDto());
            return Ok(hospitalizationDtos);
        }

    }
}
