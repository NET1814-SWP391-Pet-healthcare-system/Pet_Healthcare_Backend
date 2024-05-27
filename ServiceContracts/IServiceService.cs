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
        bool AddService(ServiceAddRequest request);
        Service? GetServiceById(int id);
        IEnumerable<Service> GetServices();
        bool UpdateService(ServiceUpdateRequest request);
        bool RemoveService(int id);
    }
}
