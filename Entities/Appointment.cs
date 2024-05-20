using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetHealthCareSystem.Data.Enum;

namespace PetHealthCareSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public User Customer { get; set; }
        public Pet Pet { get; set; }
        public User Vet { get; set; }
        public Slot Slot { get; set; }
        public Service Service { get; set; }
        public DateOnly Date { get; set; }
        public double TotalCost { get; set; }
        public DateOnly CancellationDate { get; set; }
        public double RefundAmount { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public AppointmentStatus Status{ get; set; }
    }
}