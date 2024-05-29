using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.ServiceDTO
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Cost { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
