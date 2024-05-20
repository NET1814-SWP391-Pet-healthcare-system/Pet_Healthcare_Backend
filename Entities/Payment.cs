using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetHealthCareSystem.Data.Enum;

namespace PetHealthCareSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public User Customer { get; set; }
        public Invoice Invoice { get; set; }
        public double Amount { get; set; }
        public DateOnly PaymentDate {get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
    }
}