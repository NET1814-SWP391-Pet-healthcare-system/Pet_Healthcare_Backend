using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Service
    {
        public int serviceId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public ICollection<Appointment> appointments { get; set; }
    }
}