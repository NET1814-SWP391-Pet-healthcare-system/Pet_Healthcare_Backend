using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Slot
    {
        public int slotId { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
        public TimeSpan duration => endTime - startTime;
        public ICollection<Appointment> appointments { get; set; }
    }
}