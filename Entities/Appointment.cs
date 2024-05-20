using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    public class Appointment
    {
        public int appointmentId { get; set; }
        public User customer { get; set; }
        public Pet pet { get; set; }
        public User vet { get; set; }
        public Slot slot { get; set; }
        public Service service { get; set; }
        public DateOnly date { get; set; }
        public double totalCost { get; set; }
        public DateOnly cancellationDate { get; set; }
        public double refundAmount { get; set; }
        public int rating { get; set; }
        public string comments { get; set; }
        public AppointmentStatus status{ get; set; }
    }
}