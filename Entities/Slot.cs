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
        public int slotId { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
        public TimeSpan duration => endTime - startTime;
        public ICollection<Appointment> appointments { get; set; }
    }
}