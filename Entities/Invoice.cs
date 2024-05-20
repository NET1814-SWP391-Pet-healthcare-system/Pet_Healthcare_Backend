using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int invoiceId { get; set; }
        [ForeignKey("Appointment")]
        public int appointmentId { get; set; }
        public Appointment appointment { get; set; }
        [ForeignKey("Hospitalization")]
        public int hospitalizationId { get; set; }
        public Hospitalization hospitalization { get; set; }
        public DateOnly date { get; set; }
        public double totalAmount { get; set; }
        public ICollection<Payment> payments { get; set; } 
    }
}