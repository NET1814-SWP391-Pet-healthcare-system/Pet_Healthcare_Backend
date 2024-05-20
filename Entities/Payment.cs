using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    public class Payment
    {
        [Key]
        public int paymentId { get; set; }
        [ForeignKey("User")]
        public int customerId { get; set; }
        public User customer { get; set; }
        [ForeignKey("Invoice")]
        public int invoiceId { get; set; }
        public Invoice invoice { get; set; }
        public double amount { get; set; }
        public DateOnly paymentDate { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public bool isPaid { get; set; }
    }
}