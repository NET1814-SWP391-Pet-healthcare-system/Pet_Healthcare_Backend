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
using RepositoryContracts;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPetService _petService;
        private readonly IUserService _userService;
        private readonly ISlotService _slotService;
        private readonly IServiceService _serviceService;
        private readonly UserManager<User> _userManager;
        public AppointmentController(IAppointmentService appointmentService, IUserService userService
            , UserManager<User> userManager, IPetService petService, ISlotService slotService
            , IServiceService serviceService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _userManager = userManager;
            _petService = petService;
            _slotService = slotService;
            _serviceService = serviceService;
        }

        [HttpGet]
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

        [Authorize(Policy = "CustomerOrEmployeePolicy")]
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] AppointmentAddRequest appointmentAddRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            // Get logged in customer
            var username = User.GetUsername();
            var userModel = await _userManager.FindByNameAsync(username);

            if (User.IsInRole("Customer"))
            {
                appointmentAddRequest.CustomerUserName = username;
                userModel = await _userManager.FindByNameAsync(username) as Customer;  
            }
            else if (User.IsInRole("Employee"))
            {
                userModel = await _userManager.FindByNameAsync(appointmentAddRequest.CustomerUserName) as Customer;
                if (userModel == null)
                {
                    return BadRequest("Customer does not exist");
                }
            }

            // Get customer pet
            var pet = await _petService.GetPetById(appointmentAddRequest.PetId);
            if (pet.CustomerId != userModel.Id)
            {
                return BadRequest("This pet is not yours");
            }

            // Get Slot
            var slot = await _slotService.GetSlotByIdAsync(appointmentAddRequest.SlotId);
            if (slot == null)
            {
                return BadRequest("Slot does not exist");
            }

            // Get Service
            var service = await _serviceService.GetServiceById(appointmentAddRequest.ServiceId);
            if (service == null)
            {
                return BadRequest("Service does not exist");
            }

            // Get DateOnly and Slot
            var appointmentDate = DateOnly.FromDateTime(appointmentAddRequest.Date);
            var appointmentSlot = appointmentAddRequest.SlotId;

            // Get vet
            var vetUsername = appointmentAddRequest.VetUserName;
            if (vetUsername != null)
            {
                var vet = await _userManager.FindByNameAsync(vetUsername) as Vet;
                if (vet == null)
                {
                    return BadRequest("Vet does not exist");
                }

                var appointmentsInDateAndSlot = await _appointmentService.GetAppointmentsByDateAndSlotAsync(appointmentDate, appointmentAddRequest.SlotId);

                if (appointmentsInDateAndSlot.Any(a => a.VetId == vet.Id))
                {
                    return BadRequest("The selected vet is already booked");
                }

                var appointmentModel = appointmentAddRequest.ToAppointmentFromAdd(vet);
                appointmentModel.CustomerId = userModel.Id;
                appointmentModel.Slot = slot;
                appointmentModel.Service = service;
                appointmentModel.TotalCost = (double)appointmentModel.Service.Cost;

                await _appointmentService.AddAppointmentAsync(appointmentModel);
                return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = appointmentModel.AppointmentId }, appointmentModel.ToAppointmentDto());
            }
            else 
            {
                var availableVet = await _userService.GetAvailableVetAsync(appointmentDate, appointmentSlot);
                if (availableVet == null)
                {
                    return BadRequest("No available vet for the chosen slot");
                }
                appointmentAddRequest.VetUserName = availableVet.UserName;

                var appointmentModel = appointmentAddRequest.ToAppointmentFromAdd(availableVet);
                appointmentModel.CustomerId = userModel.Id;
                appointmentModel.Slot = slot;
                appointmentModel.Service = service;
                appointmentModel.TotalCost = (double)appointmentModel.Service.Cost;

                await _appointmentService.AddAppointmentAsync(appointmentModel);
                return CreatedAtAction(nameof(GetAppointmentById), new { appointmentId = appointmentModel.AppointmentId }, appointmentModel.ToAppointmentDto());
            }
        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpPut("check-in/{appointmentId}")]
        public async Task<IActionResult> CheckIn([FromRoute] int appointmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointmentModel = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            if (appointmentModel == null)
            {
                return BadRequest("Appointment is not eligable for check-in (needs to be booked state)");
            }
            return Ok(appointmentModel.ToAppointmentDto());
        }

        [Authorize(Policy = "CustomerPolicy")]
        [HttpPut("rate/{appointmentId}")]
        public async Task<IActionResult> RateAppointment([FromRoute] int appointmentId, [FromBody] AppointmentRatingRequest AppointmentRatingRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            var appointmentModel = await _appointmentService.RateAppointmentAsync(appointmentId, AppointmentRatingRequest.ToAppointmentFromRating());
            if (appointmentModel == null)
            {
                return BadRequest("Appointment is not eligable for rating (needs to be processing state)");
            }
            return Ok(appointmentModel.ToAppointmentDto());
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] int appointmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
