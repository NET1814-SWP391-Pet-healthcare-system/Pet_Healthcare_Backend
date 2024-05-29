using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.ServiceDTO;
using ServiceContracts.Mappers;
using Services;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceService _serviceService;

        public ServiceController(ApplicationDbContext context, IServiceService serviceService)
        {
            _context = context;
            _serviceService = serviceService;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody]ServiceAddRequest serviceAddRequest)
        {
                if (serviceAddRequest == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _serviceService.AddService(serviceAddRequest.ToServiceFromAdd());
                return Ok("Added Service Successfully");
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var services = await _serviceService.GetServices();
            var serviceDtos = services.Select(x => x.ToServiceDto());
            return Ok(serviceDtos);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }


        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody]ServiceUpdateRequest serviceUpdateRequest)
        {
            var serviceData = await _serviceService.GetServiceById(id);
            if (serviceUpdateRequest == null || !ModelState.IsValid || serviceData == null)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = await _serviceService.UpdateService(id, serviceUpdateRequest.ToServiceUpdate());
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Updated Successfully");
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var isDeleted = await _serviceService.RemoveService(id);
            if (!ModelState.IsValid || isDeleted == null)
            {
                return BadRequest(ModelState);
            }
            return Ok("Nuke Success");
        }
    }
}
