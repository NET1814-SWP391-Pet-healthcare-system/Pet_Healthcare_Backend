using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentDetailService _appointmentDetailService;
        private readonly IRecordService _recordService;

        public AppointmentDetailController(IAppointmentService appointmentService, IAppointmentDetailService appointmentDetailService, IRecordService recordService)
        {
            _appointmentService = appointmentService;
            _appointmentDetailService = appointmentDetailService;
            _recordService = recordService;
        }
        //Create
        [HttpPost]
        public async Task<IActionResult> AddAppointmentDetail([FromBody] AppointmentDetailAddRequest? appointmentDetail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessResult businessResult = new BusinessResult();
            AppointmentDetail appointmentDetailModel = appointmentDetail.ToAppointmentDetailFromAdd();
            //appointmentDetailModel.RecordId = _appointmentService.GetAppointmentByIdAsync((int)appointmentDetailModel.AppointmentId).Result.Pet.RecordID;
            if (appointmentDetailModel == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail request is null";
                return NotFound(businessResult);
            }

            var data = await _appointmentDetailService.AddAppointmentDetailAsync(appointmentDetailModel);

            if(data != null)
            {
                businessResult.Data = data;
                businessResult.Message = "Successful";
                businessResult.Status = 200;
                return Ok(businessResult);
            }
            
            businessResult.Status = 500;
            businessResult.Data = null;
            businessResult.Message = "Failed to retrieve data";
            return StatusCode(StatusCodes.Status500InternalServerError, businessResult);
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetails()
        {
            BusinessResult businessResult = new BusinessResult();
            var list = await _appointmentDetailService.GetAppointmentDetailsAsync();
            if (list == null)
            {
                businessResult.Status = 500;
                businessResult.Data = null;
                businessResult.Message = "Failed to retrive data";
                return StatusCode(StatusCodes.Status500InternalServerError, businessResult);
            }
            businessResult.Data = list;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);

        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAppointmentDetailById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var appointmentDetail = await _appointmentDetailService.GetAppointmentDetailByIdAsync(id);
            if (appointmentDetail == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No AppointmentDetail found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = appointmentDetail;
            businessResult.Message = "AppointmentDetail found";
            return Ok(businessResult);
        }


        //Update
        [HttpPut("update-diagnose/{id}")]
        public async Task<IActionResult> UpdateDiagnosis([FromRoute] int id ,[FromBody]  AppointmentDetailUpdateDiagnosis? appointmentDetail)
        {
            BusinessResult businessResult = new BusinessResult();
            if (!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }

            var appointmentDetailModel = await _appointmentDetailService.GetAppointmentDetailByIdAsync(id);
            var UpdateDiagnosis = appointmentDetail.ToAppointmentDetailUpdateDiagnosis();
            appointmentDetailModel.RecordId = UpdateDiagnosis.RecordId;
            appointmentDetailModel.Record = await _recordService.GetRecordByIdAsync(id);
            appointmentDetailModel.Diagnosis = UpdateDiagnosis.Diagnosis;
            appointmentDetailModel.Medication = UpdateDiagnosis.Medication;
            appointmentDetailModel.Treatment = UpdateDiagnosis.Treatment;
            if (AppointmentDetailValidation.IsAppointmentDetailValid(appointmentDetailModel,_appointmentService,_recordService) ==false)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Invalid appointmentDetail";
                return BadRequest(businessResult);
            }
            var isUpdated = await _appointmentDetailService.UpdateAppointmentDetailAsync(id, appointmentDetailModel);
            if (isUpdated == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = isUpdated;
            businessResult.Message = "User updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{appointmentDetailId}")]
        public async Task<IActionResult>DeleteAppointmentDetailById([FromRoute] int appointmentDetailId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BusinessResult businessResult = new BusinessResult();
            var isDeleted = await _appointmentDetailService.RemoveAppointmentDetailAsync(appointmentDetailId);
            if (isDeleted != null)
            {
                businessResult.Status = 200;
                businessResult.Data = isDeleted;
                businessResult.Message = "AppointmentDetail deleted";
                return Ok(businessResult);
            }
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "AppointmentDetail not found";
            return NotFound(businessResult);
        }
    }
}
