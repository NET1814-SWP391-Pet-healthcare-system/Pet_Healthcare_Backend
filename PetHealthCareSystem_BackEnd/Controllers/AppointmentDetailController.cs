using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.ServiceDTO;
using ServiceContracts.DTO.UserDTO;
using ServiceContracts.Mappers;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    // [Authorize]
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
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            AppointmentDetail appointmentDetailModel = appointmentDetail.ToAppointmentDetailFromAdd();
            //appointmentDetailModel.RecordId = _appointmentService.GetAppointmentByIdAsync((int)appointmentDetailModel.AppointmentId).Result.Pet.RecordID;
            var appointment = await _appointmentService.GetAppointmentByIdAsync((int)appointmentDetailModel.AppointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found");
            }
            if (appointment.AppointmentDetail != null)
            {
                return BadRequest("This appointment already has an appointment detail");
            }
            if (appointmentDetailModel == null)
            {
                return NotFound("Please input data");
            }
            var record = await _recordService.GetRecordByPetIdAsync((int)appointment.PetId);
            if (record == null) 
            {
                return BadRequest("Pet doesn't have a record yet");
            }
            appointmentDetailModel.RecordId = record.RecordId;
            record.NumberOfVisits++;
            await _recordService.UpdateRecordAsync(record);

            var result = await _appointmentDetailService.AddAppointmentDetailAsync(appointmentDetailModel);
            return Ok(result.ToAppointDetailDto());
        }

        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetRecordAppointmentDetails([FromRoute] int recordId)
        {
            var recordAppointmentDetails = await _recordService.GetAppointmentDetailsAsync();
            return Ok(recordAppointmentDetails.Where(x => x.RecordId == recordId));
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetails()
        {
            var list = await _appointmentDetailService.GetAppointmentDetailsAsync();
            var appoint = list.Select(x => x.ToAppointDetailDto());
            return Ok(appoint);

        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAppointmentDetailById(int id)
        {
            var appointmentDetail = await _appointmentDetailService.GetAppointmentDetailByIdAsync(id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }
            return Ok(appointmentDetail.ToAppointDetailDto());
        }


        //Update
        [HttpPut("update-diagnose/{id}")]
        public async Task<IActionResult> UpdateDiagnosis([FromBody] AppointmentDetailUpdateDiagnosis? appointmentDetail)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
           
            var appointmentDetailModel = await _appointmentDetailService.GetAppointmentDetailByIdAsync((int)appointmentDetail.AppointmentId);
            if (appointmentDetailModel == null)
            {
                return NotFound("AppointmentDetail not found");
            }
            var UpdateDiagnosis = appointmentDetail.ToAppointmentDetailUpdateDiagnosis();
            if (await _appointmentService.GetAppointmentByIdAsync((int)appointmentDetailModel.AppointmentId) == null
                || await _recordService.GetRecordByIdAsync((int)appointmentDetail.RecordId) == null)
            {
                return NotFound("Appointment or Record not found");
            }
            var isUpdated = await _appointmentDetailService.UpdateAppointmentDetailAsync(UpdateDiagnosis);
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(isUpdated.ToAppointDetailDto());
        }

        //Delete
        [HttpDelete("{appointmentDetailId}")]
        public async Task<IActionResult> DeleteAppointmentDetailById([FromRoute] int appointmentDetailId)
        {
            var existingappoint = await _appointmentDetailService.GetAppointmentDetailByIdAsync(appointmentDetailId);
            if (existingappoint == null)
            {
                return NotFound("AppointmentDetail not found");
            }
            var isDeleted = await _appointmentDetailService.RemoveAppointmentDetailAsync(appointmentDetailId);
            if (!isDeleted)
            {
                return BadRequest("Delete fail");
            }
            return Ok(existingappoint.ToAppointDetailDto());
        }
        [HttpGet("{petId}")]
        public async Task<IActionResult> GetAppoinmentDetailOfPet([FromRoute] int petId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var listOfAppointDetail = await _appointmentDetailService.GetAppointmentDetailsAsync();
            var appointDetail = listOfAppointDetail.Where(x => x.Record.PetId == petId).Select(x => x.ToAppointDetailDto());
            return Ok(appointDetail);

        }
    }
}
