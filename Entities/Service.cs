using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}