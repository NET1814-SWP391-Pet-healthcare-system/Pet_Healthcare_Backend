using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.ServiceDTO;
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
        public async Task<Service> AddService(Service request)
        {
            return await _serviceRepository.Add(request);
        }

        public async Task<Service?> GetServiceById(int id)
        {
            var service = await _serviceRepository.GetById(id);
            return service;
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _serviceRepository.GetAll();
        }

        public async Task<Service?> RemoveService(int id)
        {
            return await _serviceRepository.Remove(id);

        }
        public async Task<Service?> GetServiceByName(string name)
        {
            return await _serviceRepository.GetByName(name);
        }
        public async Task<Service?> UpdateService(int id, Service request)
        {
            var service = await _serviceRepository.GetById(id);
            if (service == null)
            {
                return null;
            }
            service.Name = request.Name;
            service.Description = request.Description;
            service.Cost = request.Cost;
            return await _serviceRepository.Update(id,service);

        }
    }
}
