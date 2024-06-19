using Entities;
using Entities.Enum;
using ServiceContracts.DTO.AppointmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IAppointmentService
    {
        Task<Appointment> AddAppointmentAsync(Appointment appointmentModel);
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetCustomerAppointments(string customerId);
        Task<IEnumerable<Appointment>> GetVetsAppointments(string vetId);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAndSlotAsync(DateOnly date, int slotId);
        Task<Appointment?> CheckInAppointmentAsync(int id);
        Task<Appointment?> RateAppointmentAsync(int id, Appointment appointmentModel);
        Task<Appointment?> RemoveAppointmentAsync(int id);
        Task<Appointment?> UpdateAppointmentStatus(int id, AppointmentStatus appointmentStatus);
        Task<Appointment?> CancelAppointment(int id); 
    }
}
