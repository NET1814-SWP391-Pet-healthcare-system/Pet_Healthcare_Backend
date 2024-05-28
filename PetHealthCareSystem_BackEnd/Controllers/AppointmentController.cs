using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Entities;
using PetHealthCareSystem_BackEnd.Extensions;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<User> _userManager;
        public AppointmentController(IAppointmentService appointmentService, UserManager<User> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAppointments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointments = await _appointmentService.GetAppointmentsAsync();
            var appointmentDtos = appointments.Select(x => x.ToAppointmentDto());
            return Ok(appointmentDtos);
        }

        [HttpGet("{appointmentId:int}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment.ToAppointmentDto());
        }

        [HttpPost("book")]
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentAddRequest appointmentAddRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            var customerUsername = User.GetUsername();
            var customer = await _userManager.FindByNameAsync(customerUsername) as Customer;

            var vetUsername = appointmentAddRequest.VetUserName;
            var vet = await _userManager.FindByNameAsync(vetUsername) as Vet;

            var appointmentModel = appointmentAddRequest.ToAppointmentFromAdd(vet);
            appointmentModel.CustomerId = customer.Id;

            var appointmentDate = appointmentModel.Date;
            var appointmentsInDate = await _appointmentService.GetAppointmentsInDateAsync((DateOnly)appointmentDate);
            
            if (AppointmentValidation.IsDuplicateBooking(appointmentModel, appointmentsInDate))
            {
                return BadRequest("This slot has already been booked");
            }
            if (!AppointmentValidation.IsUserPet(customer, (int)appointmentModel.PetId))
            {
                return BadRequest("This is not this user's pet");
            }
            
            await _appointmentService.AddAppointmentAsync(appointmentModel);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentModel.AppointmentId }, appointmentModel.ToAppointmentDto());
        }

        [HttpPut]
        [Route("rate/{appointmentId}")]
        public async Task<IActionResult> RateAppointment([FromRoute] int appointmentId, [FromBody] AppointmentRatingRequest AppointmentRatingRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            var appointmentModel = await _appointmentService.RateAppointmentAsync(appointmentId, AppointmentRatingRequest.ToAppointmentFromRating());
            if (appointmentModel == null)
            {
                return NotFound("Appointment not found");
            }
            return Ok(appointmentModel.ToAppointmentDto());
        }

        [HttpDelete]
        [Route("{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] int appointmentId)
        {
            if (_appointmentService.GetAppointmentByIdAsync(appointmentId) == null) 
            { 
                return NotFound(); 
            }
            var appointmentModel = await _appointmentService.RemoveAppointmentAsync(appointmentId);
            if (appointmentModel == null)
            {
                return NotFound("Appointment does not exist");
            }
            return Ok(appointmentModel.ToAppointmentDto());
        }
    }
}
