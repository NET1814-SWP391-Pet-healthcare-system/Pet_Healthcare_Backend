using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public RecordController(PetService petService, IRecordService recordService)
        {
            _petService = _petService;
            _recordService = recordService;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<BusinessResult>> AddRecordAsync(RecordAddRequest? record)
        {
            BusinessResult businessResult = new BusinessResult();
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var recordModel = record.ToRecordFromAdd();
            //var IsPetHasRecord = _petService.GetPetById((int)recordModel.PetId).Result.RecordId;
            //if(IsPetHasRecord != null)
            //{
            //    businessResult.Status = 400;
            //    businessResult.Data = null;
            //    businessResult.Message = "Pet already has a record";
            //    return BadRequest(businessResult);
            //}
            if (recordModel == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Record request is null";
                return BadRequest(businessResult);
            }

            var data = await _recordService.AddRecordAsync(recordModel);
            if(data == null)
            {
                businessResult.Status = 500;
                businessResult.Data = null;
                businessResult.Message = "Failed to retrived data";
                return StatusCode(StatusCodes.Status500InternalServerError, businessResult);
            }
            businessResult.Data = data;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        //Read
        [HttpGet("records")]
        public async Task<ActionResult<BusinessResult>> GetRecords()
        {
            var list = await _recordService.GetRecordsAsync();
            BusinessResult businessResult = new BusinessResult();
            if(list == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Record found";
                return NotFound(businessResult);
            }
            businessResult.Data = list;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return  Ok(businessResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessResult>> GetRecordById(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessResult businessResult = new BusinessResult();
            var user = await _recordService.GetRecordByIdAsync(id);
            if (user == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Record found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = user;
            businessResult.Message = "Record found";
            return Ok(businessResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessResult>> RemoveRecord(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessResult businessResult = new BusinessResult();
            var record = await _recordService.RemoveRecordAsync(id);
            if (record!=null)
            {
                businessResult.Status = 200;
                businessResult.Data = record;
                businessResult.Message = "Record removed";
                return Ok(businessResult);
            }
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No Record found";
            return NotFound(businessResult);
        }

        //Update
        [HttpPut("update-record/{id}")]
        public async Task<ActionResult<BusinessResult>> UpdateRecord([FromRoute]int id, RecordUpdateRequest? record)
        {
            BusinessResult businessResult = new BusinessResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (record == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }

            var recordModel = record.ToRecordFromUpdate();

            var recordUpdated = await _recordService.UpdateRecordAsync(id, recordModel);

            if (recordUpdated !=null)
            {
                businessResult.Status = 200;
                businessResult.Data = null;
                businessResult.Message = "Record updated";
                return Ok(businessResult);
            }
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No Record found";
            return NotFound(businessResult);
        }

    }
}
