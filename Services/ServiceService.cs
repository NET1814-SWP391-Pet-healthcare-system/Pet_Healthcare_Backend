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
        public bool AddService(ServiceAddRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var service = request.ToService();
            _serviceRepository.Add(service);
            return true;
        }

        public Service? GetServiceById(int id)
        {
            var service = _serviceRepository.GetById(id);
            return service;
        }

        public IEnumerable<Service> GetServices()
        {
            return _serviceRepository.GetAll();
        }

        public bool RemoveService(int id)
        {
            var service = _serviceRepository.GetById(id);
            if (service == null)
            {
                return false;
            }
            return _serviceRepository.Remove(id);

        }

        public bool UpdateService(ServiceUpdateRequest request)
        {
            var service = _serviceRepository.GetById(request.ServiceId);
            if (service == null)
            {
                return false;
            }

            return _serviceRepository.Update(request.ToService());

        }
    }
}
