using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Vet : User
    {
        // Vet-specific properties
        public int? Rating { get; set; }
        public int? YearsOfExperience { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Hospitalization> Hospitalizations { get; set; }
    }
}
