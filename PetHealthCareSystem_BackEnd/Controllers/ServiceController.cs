using Entities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        //Create
     //   [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody]ServiceAddRequest? serviceAddRequest)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            if (serviceAddRequest == null)
                {
                    return BadRequest("Service data is required");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (serviceAddRequest.Cost < 0)
                {
                    return BadRequest("Cost cannot be negative");
                }

                if (string.IsNullOrWhiteSpace(serviceAddRequest.Name) || string.IsNullOrWhiteSpace(serviceAddRequest.Description))
                {
                    return BadRequest("Name and Description cannot be null or whitespace");
                }

                var existingService = await _serviceService.GetServiceByName(serviceAddRequest.Name);
                if (existingService != null)
                {
                    return Conflict("Service with the same name already exists");
                }
                var service = serviceAddRequest.ToServiceFromAdd();
                var result = await _serviceService.AddService(service);

                return Ok(result.ToServiceDto());
            
        }

        //Read
      //  [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetServices();
            var serviceDtos = services.Select(x => x.ToServiceDto());
            return Ok(serviceDtos);
        }
 //       [Authorize]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service.ToServiceDto());
        }


        //Update
       // [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody]ServiceUpdateRequest serviceUpdateRequest)
        {
            var serviceData = await _serviceService.GetServiceById(id);
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            if (serviceData == null)
            {
                return NotFound("Service not found");
            }
            var service = serviceUpdateRequest.ToServiceUpdate();
            service.ServiceId = id;
            var isUpdated = await _serviceService.UpdateService(service);
            if (isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(service.ToServiceDto());
        }

        //Delete
  //      [Authorize(Policy = "VetEmployeeAdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var existingService = await _serviceService.GetServiceById(id);
            if (existingService == null)
            {
                return NotFound("Service not found");
            }
            var isDeleted = await _serviceService.RemoveService(id);
            if (!isDeleted)
            {
                return BadRequest("Delete Failed");
            }
            return Ok(existingService.ToServiceDto());
        }
    }
}
