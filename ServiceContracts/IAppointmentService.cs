using Entities;
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
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByDateAndSlotAsync(DateOnly date, int slotId);
        Task<Appointment?> CheckInAppointmentAsync(int id);
        Task<Appointment?> RateAppointmentAsync(int id, Appointment appointmentModel);
        Task<Appointment?> RemoveAppointmentAsync(int id);
    }
}
