using Azure.Core;
using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.ServiceDTO;
using ServiceContracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<ServiceDTO> AddService(Service request)
        {
            await _serviceRepository.Add(request);
            return request.ToServiceDto();
        }

        public async Task<Service> GetServiceById(int id)
        {
            var service = await _serviceRepository.GetById(id);
            if(service == null)
            {
                return null;
            }
            return service; 
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _serviceRepository.GetAll();
        }

        public async Task<bool> RemoveService(int id)
        {
            var service = await _serviceRepository.GetById(id);
            if (service == null)
            {
                return false;
            }
            return await _serviceRepository.Remove(id);
            
            

        }
        public async Task<ServiceDTO?> GetServiceByName(string name)
        {
            var service =await _serviceRepository.GetByName(name);
            if(service == null)
            {
                return null;
            }
            return service.ToServiceDto();
        }
        public async Task<ServiceDTO?> UpdateService(Service request)
        {
            if (request == null)
            {
                return null;
            }
            var service = await _serviceRepository.GetById(request.ServiceId);
            if (service == null)
            {
                return null;
            }
            service.Name = request.Name == null ? service.Name  : request.Name;
            service.Description = request.Description == null ? service.Name :request.Description;
            service.Cost = request.Cost == null ? service.Cost : request.Cost;
            await _serviceRepository.Update(service);
            return service.ToServiceDto();      

        }
    }
}
