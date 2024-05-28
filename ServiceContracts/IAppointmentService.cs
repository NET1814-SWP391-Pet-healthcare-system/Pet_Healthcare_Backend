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
        bool AddAppointment(Appointment? request);
        Appointment? GetAppointmentById(int id);
        IEnumerable<Appointment> GetAppointments();
        bool UpdateAppointment(int id, Appointment? request);
        bool RemoveAppointment(int id);
    }
}
