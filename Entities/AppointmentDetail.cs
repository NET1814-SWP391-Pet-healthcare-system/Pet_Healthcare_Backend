using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class AppointmentDetail
    {
        [Key]
        public int appointmentDetailId { get; set; }
        [ForeignKey("Appointment")]
        public int appointmentId { get; set; }
        public Appointment appointment { get; set; }
        [ForeignKey("Record")]
        public int recordId { get; set; }
        public Record record { get; set; }
        public string diagnosis { get; set; }
        public string treatment { get; set; }
        public string medication { get; set; }
    }
}