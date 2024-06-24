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

        public RecordController(IPetService petService, IRecordService recordService)
        {
            _petService = petService;
            _recordService = recordService;
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
            var recordModel = record.ToRecordFromAdd();
            //var IsPetHasRecord = _petService.GetPetById((int)recordModel.PetId).Result.RecordId;
            //if(IsPetHasRecord != null)
            //{
            //    return BadRequest(businessResult);
            //}
            if(RecordValidation.IsRecordValid(recordModel, _recordService, _petService) == false)
            {
                return BadRequest("Record not valid");
            }   

            var data = await _recordService.AddRecordAsync(recordModel);
            return Ok(data.ToRecordDto());
        }

        //Read
        [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpGet("records")]
        public async Task<IActionResult> GetRecords()
        {
            var recs = await _recordService.GetRecordsAsync();
            var recDtos = recs.Select(x => x.ToRecordDto());
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
            return Ok(rec.ToRecordDto());
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
            return Ok(record.ToRecordDto());
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
            var recordModel = record.ToRecordFromUpdate();
            recordModel.RecordId = id;
            var recordUpdated = await _recordService.UpdateRecordAsync(recordModel);
            if (recordUpdated ==null)
            {
                return BadRequest(ModelState);
            }
            return Ok(recordUpdated.ToRecordDto());
        }

    }
}
