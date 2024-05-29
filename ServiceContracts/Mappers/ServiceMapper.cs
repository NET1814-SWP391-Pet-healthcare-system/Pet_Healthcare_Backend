using Entities;
using ServiceContracts.DTO.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceDTO ToServiceDto(this Service serviceModel)
        {
            return new ServiceDTO()
            {
                ServiceId = serviceModel.ServiceId,
                Name = serviceModel.Name,
                Description = serviceModel.Description, 
                Cost = serviceModel.Cost,
                Appointments = serviceModel.Appointments
            };
        }

        public static Service ToServiceFromAdd(this ServiceAddRequest serviceAddRequest)
        {
            return new Service()
            {
                Name = serviceAddRequest.Name,
                Description = serviceAddRequest.Description,
                Cost = serviceAddRequest.Cost,
            };
        }

        public static Service ToServiceUpdate(this ServiceUpdateRequest serviceUpdateRequest)
        {
            return new Service()
            {
                Name = serviceUpdateRequest.Name,
                Description = serviceUpdateRequest.Description,
                Cost = serviceUpdateRequest.Cost,
            };
        }
    }
}
