using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.RecordDTO;
using ServiceContracts.DTO.Result;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RecordService _recordService;

        public RecordController(ApplicationDbContext context, RecordService recordService)
        {
            _context = context;
            _recordService = recordService;
        }

        //Create
        [HttpPost]
        public ActionResult<BusinessResult> AddRecord(Record? record)
        {
            BusinessResult businessResult = new BusinessResult();
            if (record == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Record request is null";
                return BadRequest(businessResult);
            }

            _recordService.AddRecordAsync(record);
            businessResult.Data = record;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        //Read
        [HttpGet("records")]
        public ActionResult<BusinessResult> GetRecords()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _recordService.GetRecordsAsync();
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetRecordById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var user = _recordService.GetRecordByIdAsync(id);
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
        public ActionResult<BusinessResult> RemoveRecord(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var record = _recordService.RemoveRecordAsync(id);
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
        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdateRecord(int id, Record? record)
        {
            BusinessResult businessResult = new BusinessResult();
            if (record == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            var recordUpdated = _recordService.UpdateRecordAsync(id, record);

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
