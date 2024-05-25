using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _AppointmentService;
        public AppointmentController(IAppointmentService AppointmentService)
        {
            _AppointmentService = AppointmentService;
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _AppointmentService.GetAppointments();
            businessResult.Status = 200;
            businessResult.Message = "Success";
            return Ok(businessResult);
        }

        [HttpGet("{AppointmentId}")]
        public IActionResult GetAppointmentById(int AppointmentId)
        {
            var Appointment = _AppointmentService.GetAppointmentById(AppointmentId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Appointment);
        }

        [HttpPost]
        public IActionResult AddAppointment(int customerId, int petId, int? vetId, int slotId, int serviceId, [FromBody] AppointmentAddRequest AppointmentAddRequest)
        {
            if (AppointmentAddRequest == null) { return BadRequest(ModelState); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _AppointmentService.AddAppointment(AppointmentAddRequest.ToAppointment(customerId, petId, vetId, slotId, serviceId));
            return Ok("successfully created");
        }

        [HttpPut("{AppointmentId}")]
        public IActionResult UpdateAppointment(int AppointmentId, [FromBody] AppointmentRatingRequest AppointmentRatingRequest)
        {
            if (AppointmentRatingRequest == null) { return BadRequest(ModelState); }
            if (_AppointmentService.GetAppointmentById(AppointmentId) == null) { return NotFound(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_AppointmentService.UpdateAppointment(AppointmentId, AppointmentRatingRequest.ToAppointment()))
            {
                ModelState.AddModelError("", "Something went wrong updating Appointment");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully updated");
        }

        [HttpDelete("{AppointmentId}")]
        public IActionResult DeleteAppointment(int AppointmentId)
        {
            if (_AppointmentService.GetAppointmentById(AppointmentId) == null) { return NotFound(); }
            if (!_AppointmentService.RemoveAppointment(AppointmentId))
            {
                ModelState.AddModelError("", "Something went wrong deleting Appointment");
            }
            return Ok("successfully deleted");
        }
    }
}
