using Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    // Add request as a customer
    public class AppointmentAddRequest
    {
        [Required(ErrorMessage = "Pet ID is required.")]
        public int PetId { get; set; }

        public string? VetUserName { get; set; }

        [Required(ErrorMessage = "Slot ID is required.")]
        public int SlotId { get; set; }

        [Required(ErrorMessage = "Service ID is required.")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date, ErrorMessage ="Invalid Date Formate (yyyy-MM-dd)")]
        public DateTime Date { get; set; }
    }
}
