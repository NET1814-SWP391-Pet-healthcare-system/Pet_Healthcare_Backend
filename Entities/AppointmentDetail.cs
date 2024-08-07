using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("AppointmentDetail")]
    public class AppointmentDetail
    {
        [Key]
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public int? RecordId { get; set; }
        [ForeignKey("RecordId")]
        public Record? Record { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medication { get; set; }
    }
}