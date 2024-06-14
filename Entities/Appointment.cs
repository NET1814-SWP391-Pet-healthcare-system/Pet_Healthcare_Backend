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
        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int? PetId { get; set; }
        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        public string? VetId { get; set; }
        [ForeignKey("VetId")]
        public Vet? Vet { get; set; }
        public int? SlotId { get; set; }
        [ForeignKey("SlotId")]
        public Slot? Slot { get; set; }
        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        public DateOnly Date { get; set; }
        public double TotalCost { get; set; }
        public DateOnly? CancellationDate { get; set; }
        public double? RefundAmount { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
        public AppointmentStatus Status{ get; set; }
        public bool? isPaid { get; set; }
    }
}