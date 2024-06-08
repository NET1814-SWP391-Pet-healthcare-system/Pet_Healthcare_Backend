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
        Task<Service> AddService(Service request);
        Task<Service?> GetServiceById(int id);
        Task<IEnumerable<Service>> GetServices();
        Task<Service?> UpdateService(int id,Service request);
        Task<Service?> RemoveService(int id);
        Task<Service?> GetServiceByName(string name);
    }
}
