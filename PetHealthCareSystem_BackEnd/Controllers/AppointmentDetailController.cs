using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO.AppointmentDetailDTO;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AppointmentDetailService _appointmentDetailService;

        public AppointmentDetailController(ApplicationDbContext context, AppointmentDetailService appointmentDetailService)
        {
            _context = context;
            _appointmentDetailService = appointmentDetailService;
        }

        [HttpPost]
        public IActionResult AddAppointmentDetail(AppointmentDetailAddRequest? appointmentDetailAddRequest)
        {
            if(appointmentDetailAddRequest == null)
            {
                return BadRequest("AppointmentDetailRequest is null");
            }

            _appointmentDetailService.AddAppointmentDetail(appointmentDetailAddRequest);

            return Ok("Created successfully");
        }

        [HttpGet("{id}")]
        public ActionResult<AppointmentDetail> GetAppointmentDetailById(int id)
        {
            var appointmentDetail = _appointmentDetailService.GetAppointmentDetail(id);
            if(appointmentDetail == null)
            {
                return BadRequest("AppointmentDetail not found");
            }
            return Ok(appointmentDetail);
        }

    }
}
