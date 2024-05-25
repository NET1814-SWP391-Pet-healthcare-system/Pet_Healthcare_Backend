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
    public class AppointmentAddRequest
    {
        [Required(ErrorMessage = "Must choose a date for the appointment")]
        public DateOnly Date { get; set; }

        public Appointment ToAppointment(int customerId, int petId, int? vetId, int slotId, int serviceId)
        {
            return new Appointment()
            { 
                CustomerId = customerId,
                PetId = petId,
                VetId = vetId,
                SlotId = slotId,
                ServiceId = serviceId,
                Date = Date,
                Status = AppointmentStatus.Boooked,
            };
        }
    }
}
