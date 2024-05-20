using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetHealthCareSystem.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set;}
        public Appointment Appointment {get; set;}
        public Hospitalization Hospitalization {get; set;}
        public DateOnly Date {get; set;}
        public double TotalAmount {get; set;}
        public ICollection<Payment> Payments {get; set;} 
    }
}