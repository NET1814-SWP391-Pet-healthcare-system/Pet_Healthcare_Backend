using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.VaccineDTO;
using ServiceContracts.Mappers;
using Services;
using System.Net;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccineController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetVaccines()
        {
            var vaccineList = await _vaccineService.GetAllVaccines();
            var result = vaccineList.Select(vaccine => vaccine.ToVaccineDto());

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccineById(int id)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vaccine = await _vaccineService.GetVaccineById(id);
            if(vaccine == null)
            {
                return NotFound("Vaccine not found");
            }
            return Ok(vaccine.ToVaccineDto());
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpPost]
        public async Task<IActionResult> AddVaccine(VaccineAddRequest vaccineAddRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var vaccine = await _vaccineService.AddVaccine(vaccineAddRequest.ToVaccineFromAdd());
            return Ok(vaccine.ToVaccineDto());
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccineById(int id)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingVaccine = await _vaccineService.GetVaccineById(id);
            if (existingVaccine is null)
            {
                return NotFound("Vaccine not found");
            }
            var isDeleted = await _vaccineService.RemoveVaccineById(id);
            if(!isDeleted)
            {
                return BadRequest("Vaccine deletion failed");
            }
            return Ok(existingVaccine.ToVaccineDto());
        }

        [Authorize(Policy = "AdminEmployeePolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest)
        {
            if(!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var existingVaccine = await _vaccineService.GetVaccineById(id);
            if(existingVaccine is null)
            {
                return NotFound("VaccineId not found");
            }
            Vaccine vaccine = vaccineUpdateRequest.ToVaccineFromUpdate();
            vaccine.VaccineId = id;
            var result = await _vaccineService.UpdateVaccine(vaccine);
            if(result == null)
            {
                return NotFound("Vaccine not found");
            }
            return Ok(result.ToVaccineDto());
        }

    }
}
