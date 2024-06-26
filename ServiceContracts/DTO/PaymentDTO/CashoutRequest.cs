using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PaymentDTO
{
    public class CashRequest
    {
        public ClaimsPrincipal customerId  { get; set; }
        public int hospitalizationId { get; set; }
    }
}
