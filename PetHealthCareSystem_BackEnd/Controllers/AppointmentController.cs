using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointments = _AppointmentService.GetAppointments();
            var appointmentDtos = appointments.Select(x => x.ToAppointmentDto());
            return Ok(appointmentDtos);
        }

        [HttpGet("{appointmentId}")]
        public IActionResult GetAppointmentById(int appointmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = _AppointmentService.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment.ToAppointmentDto());
        }

        [HttpPost("Book")]
        public IActionResult AddAppointment([FromBody] AppointmentAddRequest appointmentAddRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            if (appointmentAddRequest == null) { return BadRequest(ModelState); }
            _AppointmentService.AddAppointment(appointmentAddRequest.ToAppointmentFromAdd());
            return Ok("successfully created");
        }

        [HttpPut("Rate/{AppointmentId}")]
        public IActionResult RateAppointment(int AppointmentId, [FromBody] AppointmentRatingRequest AppointmentRatingRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            if (!_AppointmentService.UpdateAppointment(AppointmentId, AppointmentRatingRequest.ToAppointmentFromRating()))
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
