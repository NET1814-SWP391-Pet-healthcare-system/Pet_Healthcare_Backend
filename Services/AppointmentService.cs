using Azure.Core;
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

        public async Task<Appointment> AddAppointmentAsync(Appointment appointmentModel)
        {
            return await _appointmentRepository.AddAsync(appointmentModel);
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment?> RemoveAppointmentAsync(int id)
        {
            return await _appointmentRepository.RemoveAsync(id);
        }

        public async Task<Appointment?> RateAppointmentAsync(int id, Appointment appointmentModel)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null) 
            {
                return null;
            }
            existingAppointment.Comments = appointmentModel.Comments;
            existingAppointment.Rating = appointmentModel.Rating;
            existingAppointment.Status = appointmentModel.Status;
            return await _appointmentRepository.UpdateAsync(existingAppointment);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsInDateAsync(DateOnly date)
        {
            return await _appointmentRepository.GetByDateAsync(date);
        }
    }
}
