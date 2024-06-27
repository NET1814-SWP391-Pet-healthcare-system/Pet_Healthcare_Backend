using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PaymentDTO
{
    public class CashOutDto
    {
        public int? Id { get; set; }
        public string? CustomerId { get; set; }
        public string? TransactionId { get; set; }
        public int? AppointmentId { get ; set; }
        public int? HospitalizationId { get; set; }
        public DateTime? Date { get; set; }
        public double? Amount { get; set; }

    }
}
