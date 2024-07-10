using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public RecordController(IPetService petService, IRecordService recordService, IAppointmentDetailService appointmentDetailService)
        {
            _petService = petService;
            _recordService = recordService;
            _appointmentDetailService = appointmentDetailService;
        }

        //Create
        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddRecordAsync(RecordAddRequest? record)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var IsPetHasRecord = await _recordService.GetRecordByPetIdAsync(record.PetId);
            if (IsPetHasRecord != null)
            {
                return BadRequest("Pet already record");
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

        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRecord(int id)
        {
            var record = await _recordService.GetRecordByIdAsync(id);
            if (record==null)
            {
                return NotFound ("Record not found");
            }
            var isDeleted = await _recordService.RemoveRecordAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Delete Fail");
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

    }
}
