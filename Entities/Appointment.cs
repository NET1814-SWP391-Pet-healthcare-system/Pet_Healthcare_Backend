using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    public class Appointment
    {
        [Key]
        public int appointmentId { get; set; }
        [ForeignKey("User")]
        public int customerId { get; set; }
        public User customer { get; set; }
        [ForeignKey("Pet")]
        public int petId { get; set; }
        public Pet pet { get; set; }
        [ForeignKey("Vet")]
        public int vetId { get; set; }
        public User vet { get; set; }
        [ForeignKey("Slot")]
        public int slotId { get; set; }
        public Slot slot { get; set; }
        [ForeignKey("Service")]
        public int serviceId { get; set; }
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