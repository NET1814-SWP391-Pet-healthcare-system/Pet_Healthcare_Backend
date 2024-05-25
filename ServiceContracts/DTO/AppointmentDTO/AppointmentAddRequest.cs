using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentAddRequest
    {
        public int AppointmentId { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int? PetId { get; set; }
        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        public int? VetId { get; set; }
        [ForeignKey("VetId")]
        public Vet? Vet { get; set; }
        public int? SlotId { get; set; }
        [ForeignKey("SlotId")]
        public Slot? Slot { get; set; }
        public int? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        public DateOnly? Date { get; set; }
        public double? TotalCost { get; set; }
        public DateOnly? CancellationDate { get; set; }
        public double? RefundAmount { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
    }
}
