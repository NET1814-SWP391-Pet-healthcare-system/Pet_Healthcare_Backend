using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PaymentDTO
{
    public class CheckoutRequest
    {
        public int AppointmentId { get; set; }
        public string Nonce { get; set; }
    }
}
