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
        public bool AddAppointment(Appointment? request)
        {
            return _appointmentRepository.Add(request);
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public bool RemoveAppointment(int id)
        {
            return _appointmentRepository.Remove(id);
        }

        public bool UpdateAppointment(int id, Appointment? request)
        {
            var existingAppointment = _appointmentRepository.GetById(id);
            if (existingAppointment == null) { return false; }
            if (request == null) { return false; }

            existingAppointment.Comments = request.Comments;
            existingAppointment.Rating = request.Rating;
            existingAppointment.Status = request.Status;
            return _appointmentRepository.Update(existingAppointment);
        }
    }
}
