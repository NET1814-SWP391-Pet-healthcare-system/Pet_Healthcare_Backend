using Entities;
using ServiceContracts.DTO.HospitalizationDTO;
using ServiceContracts.DTO.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IServiceService
    {
        Task<ServiceDTO> AddService(Service request);
        Task<Service?> GetServiceById(int id);
        Task<IEnumerable<Service>> GetServices();
        Task<ServiceDTO?> UpdateService(Service request);
        Task<bool> RemoveService(int id);
        Task<ServiceDTO?> GetServiceByName(string name);
    }
}
