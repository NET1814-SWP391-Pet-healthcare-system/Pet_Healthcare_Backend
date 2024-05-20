using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class AppointmentDetail
    {
        public int appointmentDetailId { get; set; }
        public Appointment appointment { get; set; }
        public Record record { get; set; }
        public string diagnosis { get; set; }
        public string treatment { get; set; }
        public string medication { get; set; }
    }
}