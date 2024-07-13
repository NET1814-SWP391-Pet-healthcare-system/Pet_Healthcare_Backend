using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts;
using ServiceContracts.DTO.RecordDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IRecordService _recordService;
        private readonly IAppointmentDetailService _appointmentDetailService;
        private readonly UserManager<User> _userManager;

        public RecordController(IPetService petService, IRecordService recordService, IAppointmentDetailService appointmentDetailService, UserManager<User> userManager)
        {
            _petService = petService;
            _recordService = recordService;
            _appointmentDetailService = appointmentDetailService;
            _userManager = userManager;
        }

        //Create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRecordAsync(RecordAddRequest? record)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            if (this.User.IsInRole("Customer"))
            {
                var petIdList = await CurrentCustomerPetIdList();
                if (!petIdList.Contains(record.PetId))
                {
                    return NotFound("Customer doesn't have this pet");
                }
            }

            var IsPetHasRecord = await _recordService.GetRecordByPetIdAsync(record.PetId);
            if (IsPetHasRecord != null)
            {
                return BadRequest("Pet already has a record");
            }
            var recordModel = record.ToRecordFromAdd(_appointmentDetailService);
            recordModel.Pet = await _petService.GetPetById((int)recordModel.PetId);
           

            var data = await _recordService.AddRecordAsync(recordModel);
            return Ok(data.ToRecordDto(_appointmentDetailService));
        }

        //Read
        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpGet("records")]
        public async Task<IActionResult> GetRecords()
        {
            var recs = await _recordService.GetRecordsAsync();
            var recDtos = recs.Select(x => x.ToRecordDto(_appointmentDetailService));

            return Ok(recDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            var rec = await _recordService.GetRecordByIdAsync(id);
            if (rec == null)
            {
                return NotFound("Record not found");
            }
            if(rec.PetId == null)
            {
                return BadRequest("Record doesn't assign to any pet");
            }
       
            return Ok(rec.ToRecordDto(_appointmentDetailService));
        }

        [Authorize]
        [HttpDelete("{petId}")]
        public async Task<IActionResult> RemoveRecordByPetId(int petId)
        {
            var record = await _recordService.GetRecordByPetIdAsync(petId);
            if (record is null)
            {
                return NotFound ("Record not found");
            }

            var petIdList = await CurrentCustomerPetIdList();
            if (!petIdList.Contains(petId))
            {
                return NotFound("Customer doesn't have this pet");
            }

            try
            {
                var isDeleted = await _recordService.RemoveRecordAsync(record.RecordId);
                if (!isDeleted)
                {
                    return BadRequest("Delete failed");
                }
            }
            catch (DbUpdateException dbe)
            {
                return BadRequest("There is an appointment using this record");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
            
            return Ok(record.ToRecordDto(_appointmentDetailService));
        }

        //Update
        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPut("update-record/{id}")]
        public async Task<IActionResult> UpdateRecord([FromRoute]int id, RecordUpdateRequest record)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }
            var existingRec = await _recordService.GetRecordByIdAsync(id);
            if (existingRec == null)
            {
                return NotFound("Record not found");
            }
            var recordModel = record.ToRecordFromUpdate(_appointmentDetailService);

            existingRec.NumberOfVisits = recordModel.NumberOfVisits;
            existingRec.PetId = recordModel.PetId;
            
            var recordUpdated = await _recordService.UpdateRecordAsync(existingRec);
            if (recordUpdated ==null)
            {
                return BadRequest(ModelState);
            }
            return Ok(recordUpdated.ToRecordDto(_appointmentDetailService));
        }
        private async Task<IEnumerable<int>> CurrentCustomerPetIdList()
        {
            var currentCustomer = await _userManager.GetUserAsync(this.User);

            var customerPets = await _petService.GetCustomerPet(currentCustomer?.Id);
            var customerPetIds = customerPets.Select(p => p.PetId);
            return customerPetIds;
        }
    }
}
