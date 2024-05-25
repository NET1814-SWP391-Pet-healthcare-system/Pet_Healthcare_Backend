using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public bool AddAppointment(AppointmentAddRequest? request)
        {
            throw new NotImplementedException();
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public bool RemoveAppointment(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppointment(int id, AppointmentUpdateRequest? request)
        {
            throw new NotImplementedException();
        }
    }
}
