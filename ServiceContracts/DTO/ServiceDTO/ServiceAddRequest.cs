using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.ServiceDTO
{
    public class ServiceAddRequest
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Cost { get; set; }

        public Service ToService()
        {
            return new Service
            {
                Name = Name,
                Description = Description,
                Cost = Cost
            };
        }
    }
}
