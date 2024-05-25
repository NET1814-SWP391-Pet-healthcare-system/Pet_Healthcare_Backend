using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentUpdateRequest
    {
        [Required(ErrorMessage = "must enter date for appointment")]
        public DateOnly Date { get; set; }
        [Range(double.Epsilon, double.MaxValue)]
        public double TotalCost { get; set; }
        public DateOnly? CancellationDate { get; set; }
        public double? RefundAmount { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
    }
}
