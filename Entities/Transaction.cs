using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public int? AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }
        public int? HospitalizationId { get; set; }
        [ForeignKey("HospitalizationId")]
        public Hospitalization? Hospitalization { get; set; }
        public string? TransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
