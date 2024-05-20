using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Record
    {
        public int RecordId { get; set; }
        public Pet Pet { get; set; }
        public int NumberOfVisits { get; set; }
        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }
}