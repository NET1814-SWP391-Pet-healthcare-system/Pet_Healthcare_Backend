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
        public ActionResult<BusinessResult> AddAppointmentDetail(AppointmentDetailAddRequest? appointmentDetailAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (appointmentDetailAddRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail request is null";
                return BadRequest(businessResult);
            }

            _appointmentDetailService.AddAppointmentDetail(appointmentDetailAddRequest);
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No AppointmentDetail found";
            return Ok(businessResult);
        }

        //Read
        [HttpGet]
        public ActionResult<BusinessResult> GetAppointmentDetails()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _appointmentDetailService.GetAppointmentDetails();
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetAppointmentDetailById(int id)
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
        public ActionResult<BusinessResult> UpdateAppointmentDetail(AppointmentDetailUpdateRequest? appointmentDetailUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (appointmentDetailUpdateRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            var isUpdated = appointmentDetailUpdateRequest.UpdateAppointmentDetail();
            if (isUpdated == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = appointmentDetailUpdateRequest.UpdateAppointmentDetail();
            businessResult.Message = "User updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{username}")]
        public ActionResult<BusinessResult> DeleteUserByUsername(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var userData = _appointmentDetailService.GetAppointmentDetailById(id);
            var isDeleted = _appointmentDetailService.RemoveAppointmentDetail(id);
            if (!isDeleted)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "AppointmentDetail not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = userData;
            businessResult.Message = "AppointmentDetail deleted";
            return Ok(businessResult);
        }
    }
}
