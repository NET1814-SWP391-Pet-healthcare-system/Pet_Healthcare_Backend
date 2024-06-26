using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PaymentDTO
{
    public class CashRequest
    {
        public string customerId  { get; set; }

        public int ammount { get; set; }
    }
}
