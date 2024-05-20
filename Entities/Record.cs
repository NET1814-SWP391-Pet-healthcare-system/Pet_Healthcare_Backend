using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Record
    {
        [Key]
        public int recordId { get; set; }
        [ForeignKey("Pet")]
        public int petId { get; set; }
        public Pet pet { get; set; }
        public int numberOfVisits { get; set; }
        public ICollection<AppointmentDetail> appointmentDetails { get; set; }
    }
}