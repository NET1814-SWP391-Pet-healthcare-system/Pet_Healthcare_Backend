using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Customer : User
    {
        // Customer-specific properties
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
