using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    public class Payment
    {
        public int paymentId { get; set; }
        public User customer { get; set; }
        public Invoice invoice { get; set; }
        public double amount { get; set; }
        public DateOnly paymentDate { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public bool isPaid { get; set; }
    }
}