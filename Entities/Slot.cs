using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Slot")]
    public class Slot
    {
        [Key]
        public int SlotId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeSpan? Duration => EndTime - StartTime;
        public ICollection<Appointment>? Appointments { get; set; }
    }
}