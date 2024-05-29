using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.UserDTO;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppointmentDetailService _appointmentDetailService;

        //Create
        [HttpPost]
        public async Task<IActionResult> AddAppointmentDetail(AppointmentDetail? appointmentDetail)
        {
            BusinessResult businessResult = new BusinessResult();
            if (appointmentDetail == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail request is null";
                return BadRequest(businessResult);
            }

            _appointmentDetailService.AddAppointmentDetail(appointmentDetail);
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No AppointmentDetail found";
            return Ok(businessResult);
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAppointmentDetails()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _appointmentDetailService.GetAppointmentDetails();
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAppointmentDetailById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var user = _appointmentDetailService.GetAppointmentDetailById(id);
            if (user == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No User found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = user;
            businessResult.Message = "AppointmentDetail found";
            return Ok(businessResult);
        }


        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnosis([FromBody] AppointmentDetailUpdateDiagnosis appointmentDetailUpdateDiagnosis)
        {
            BusinessResult businessResult = new BusinessResult();
            if (!ModelState.IsValid)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            var isUpdated = appointmentDetailUpdateDiagnosis;
            if (isUpdated == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = appointmentDetailUpdateDiagnosis;
            businessResult.Message = "User updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{appointmentDetail}")]
        public async Task<IActionResult> DeleteUserByUsername([FromRoute] int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var userData = await _appointmentDetailService.GetAppointmentDetailById(id);
            var isDeleted = await _appointmentDetailService.RemoveAppointmentDetail(id);
            if (isDeleted == null)
            {
                businessResult.Status = 200;
                businessResult.Data = userData;
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
