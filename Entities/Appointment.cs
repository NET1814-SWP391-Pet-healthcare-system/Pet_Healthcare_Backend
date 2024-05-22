using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [ForeignKey("User")]
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        [ForeignKey("User")]
        public int VetId { get; set; }
        public User Vet { get; set; }
        [ForeignKey("Slot")]
        public int SlotId { get; set; }
        public Slot Slot { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
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