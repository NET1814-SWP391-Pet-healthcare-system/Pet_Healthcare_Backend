using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class AppointmentDetail
    {
        public int AppointmentDetailId { get; set; }
        public Appointment Appointment{ get; set; }
        public Record Record{ get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Medication { get; set; }
    }
}