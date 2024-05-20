using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Record
    {
        public int recordId { get; set; }
        public Pet pet { get; set; }
        public int numberOfVisits { get; set; }
        public ICollection<AppointmentDetail> appointmentDetails { get; set; }
    }
}