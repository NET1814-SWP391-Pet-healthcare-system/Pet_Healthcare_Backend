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
        public int AppointmentDetailId { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        [ForeignKey("Record")]
        public int RecordId { get; set; }
        public Record Record { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Medication { get; set; }
    }
}