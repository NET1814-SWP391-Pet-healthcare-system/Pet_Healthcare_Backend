using Entities.Enum;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string? Customer { get; set; }
        public string? Pet { get; set; }
        public string? Vet { get; set; }
        public TimeOnly? SlotStartTime { get; set; }
        public TimeOnly? SlotEndTime { get; set; }
        public string? Service { get; set; }
        public DateOnly? Date { get; set; }
        public double? TotalCost { get; set; }
        public DateOnly? CancellationDate { get; set; }
        public double? RefundAmount { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
        public string? Status { get; set; }
    }
}
