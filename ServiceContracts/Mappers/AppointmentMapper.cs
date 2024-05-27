using Entities;
using Entities.Enum;
using ServiceContracts.DTO.AppointmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceContracts.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentDto ToAppointmentDto(this Appointment appointmentModel)
        {
            return new AppointmentDto()
            {
                AppointmentId = appointmentModel.AppointmentId,
                Customer = appointmentModel.Customer?.Username,
                Pet = appointmentModel.Pet?.Name,
                Vet = appointmentModel.Vet?.Username,
                SlotStartTime = appointmentModel.Slot?.StartTime,
                SlotEndTime = appointmentModel.Slot?.EndTime,
                Service = appointmentModel.Service?.Name,
                Date = appointmentModel.Date,
                TotalCost = appointmentModel.TotalCost,
                CancellationDate = appointmentModel.CancellationDate,
                RefundAmount = appointmentModel.RefundAmount,
                Rating = appointmentModel.Rating,
                Comments = appointmentModel.Comments,
                Status = appointmentModel.Status.ToString(),
            };
        }

        public static Appointment ToAppointmentFromAdd(this AppointmentAddRequest appointmentAddRequest)
        {
            return new Appointment()
            {
                CustomerId = appointmentAddRequest.CustomerId,
                PetId = appointmentAddRequest.PetId,
                VetId = appointmentAddRequest.VetId,
                SlotId = appointmentAddRequest.SlotId,
                ServiceId = appointmentAddRequest.ServiceId,
                Date = DateOnly.FromDateTime(appointmentAddRequest.Date),
                Status = AppointmentStatus.Boooked,
            };
        }

        public static Appointment ToAppointmentFromRating(this AppointmentRatingRequest appointmentRatingRequest)
        {
            return new Appointment()
            {
                Rating = appointmentRatingRequest.Rating,
                Comments = appointmentRatingRequest.Comments,
                Status = AppointmentStatus.Done,
            };
        }
    }
}
