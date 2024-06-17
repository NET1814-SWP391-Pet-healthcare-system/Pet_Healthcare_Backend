using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction
    {
        public string CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public required string TransactionId { get; set; }

    }
}
