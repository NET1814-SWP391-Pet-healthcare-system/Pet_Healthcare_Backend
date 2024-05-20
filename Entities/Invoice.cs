using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Invoice
    {
        public int invoiceId { get; set;}
        public Appointment appointment { get; set;}
        public Hospitalization hospitalization { get; set;}
        public DateOnly date { get; set;}
        public double totalAmount { get; set;}
        public ICollection<Payment> payments { get; set;} 
    }
}