using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Slot
    {
        public int SlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
        public ICollection<Appointment> Appointments { get; set; }
    }
}