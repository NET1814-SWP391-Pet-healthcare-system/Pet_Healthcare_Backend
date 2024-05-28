using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.DTO.ServiceDTO;

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
        public ActionResult<BusinessResult> AddService(ServiceAddRequest? serviceAddRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (serviceAddRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Service request is null";
                return BadRequest(businessResult);
            }

            _serviceService.AddService(serviceAddRequest);
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = "No Service found";
            return Ok(businessResult);
        }

        //Read
        [HttpGet]
        public ActionResult<BusinessResult> GetServices()
        {
            BusinessResult businessResult = new BusinessResult();
            businessResult.Data = _serviceService.GetServices(); ;
            businessResult.Message = "Successful";
            businessResult.Status = 200;
            return Ok(businessResult);
        }

        [HttpGet("id/{id}")]
        public ActionResult<BusinessResult> GetServiceById(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var service = _serviceService.GetServiceById(id);
            if (service == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Service found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = service;
            businessResult.Message = "Service found";
            return Ok(businessResult);
        }


        //Update
        [HttpPut("{id}")]
        public ActionResult<BusinessResult> UpdateServiceById(int id, ServiceUpdateRequest? serviceUpdateRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            if (serviceUpdateRequest == null)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Request is null";
                return BadRequest(businessResult);
            }
            if (id != serviceUpdateRequest.ServiceId)
            {
                businessResult.Status = 400;
                businessResult.Data = null;
                businessResult.Message = "Mismatched id";
                return BadRequest(businessResult);
            }
            var isUpdated = _serviceService.UpdateService(serviceUpdateRequest);
            if (!isUpdated)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Service not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = serviceUpdateRequest.ToService();
            businessResult.Message = "Service updated";
            return Ok(businessResult);
        }

        //Delete
        [HttpDelete("{servicename}")]
        public ActionResult<BusinessResult> DeleteServiceByServicename(int id)
        {
            BusinessResult businessResult = new BusinessResult();
            var serviceData = _serviceService.GetServiceById(id);
            var isDeleted = _serviceService.RemoveService(id);
            if (!isDeleted)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Service not found";
                return NotFound(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = serviceData;
            businessResult.Message = "Service deleted";
            return Ok(businessResult);
        }
    }
}
