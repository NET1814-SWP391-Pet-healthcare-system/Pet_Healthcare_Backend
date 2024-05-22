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
        public int InvoiceId { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        [ForeignKey("Hospitalization")]
        public int HospitalizationId { get; set; }
        public Hospitalization Hospitalization { get; set; }
        public DateOnly Date { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<Payment> Payments { get; set; } 
    }
}