using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts;

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
            var Appointments = _AppointmentService.GetAppointments();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Appointments);
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
        public IActionResult AddAppointment([FromBody] AppointmentAddRequest AppointmentAddRequest)
        {
            if (AppointmentAddRequest == null) { return BadRequest(ModelState); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _AppointmentService.AddAppointment(AppointmentAddRequest);
            return Ok("successfully created");
        }

        [HttpPut("{AppointmentId}")]
        public IActionResult UpdateAppointment(int AppointmentId, [FromBody] AppointmentUpdateRequest AppointmentUpdateRequest)
        {
            if (AppointmentUpdateRequest == null) { return BadRequest(ModelState); }
            if (_AppointmentService.GetAppointmentById(AppointmentId) == null) { return NotFound(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_AppointmentService.UpdateAppointment(AppointmentId, AppointmentUpdateRequest))
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
