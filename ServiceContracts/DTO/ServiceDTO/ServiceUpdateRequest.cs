using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO.ServiceDTO
{
    public class ServiceUpdateRequest
    {
        [Required]
        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Cost { get; set; }

        public Service ToService()
        {
            return new Service
            {
                ServiceId = ServiceId,
                Name = Name,
                Description = Description,
                Cost = Cost
            };
        }

    }
}
