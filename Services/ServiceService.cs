﻿using Azure.Core;
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
        public async Task<Service?> AddService(Service request)
        {
            if (request == null)
            {
                return null;
            }
            await _serviceRepository.Add(request);
            return request;
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
            return await _serviceRepository.Remove(service);
            
            

        }
        public async Task<Service?> GetServiceByName(string name)
        {
            var service =await _serviceRepository.GetByName(name);
            if(service == null)
            {
                return null;
            }
            return service;
        }
        public async Task<Service?> UpdateService(Service request)
        {
            if (request == null)
            {
                return null;
            }
            var service = await _serviceRepository.GetById(request.ServiceId);
            service.Name = request.Name == null ? service.Name  : request.Name;
            service.Description = request.Description == null ? service.Name :request.Description;
            service.Cost = request.Cost == null ? service.Cost : request.Cost;
            await _serviceRepository.Update(service);
            return service;      

        }
    }
}
