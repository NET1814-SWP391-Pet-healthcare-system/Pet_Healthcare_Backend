using Azure.Core;
using Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            if (existingAppointment == null || existingAppointment.Status != AppointmentStatus.Done && existingAppointment.Status != AppointmentStatus.Processing)
            {
                return null;
            }
            existingAppointment.Comments = appointmentModel.Comments;
            existingAppointment.Rating = appointmentModel.Rating;
            existingAppointment.Status = appointmentModel.Status;
            return await _appointmentRepository.UpdateAsync(existingAppointment);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAndSlotAsync(DateOnly date, int slotId)
        {
            return await _appointmentRepository.GetByDateAndSlotAsync(date, slotId);
        }

        public async Task<Appointment?> CheckInAppointmentAsync(int id)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if(existingAppointment == null || existingAppointment.Status != AppointmentStatus.Boooked)
            {
                return null;
            }
            existingAppointment.Status = AppointmentStatus.Processing;
            return await _appointmentRepository.UpdateAsync(existingAppointment);
        }

        public async Task<IEnumerable<Appointment>> GetCustomerAppointments(string customerId)
        {
            return await _appointmentRepository.GetByUserIdAsync(customerId);
        }

        public async Task<IEnumerable<Appointment>> GetVetsAppointments(string vetId)
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return appointments.Where(a => a.VetId == vetId);
        }

        public async Task<Appointment?> UpdateAppointmentStatus(int id, AppointmentStatus status)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if(existingAppointment == null)
            {
                return null;
            }
            return await _appointmentRepository.UpdateAppointmentStatusAsync(id, status);
        }
        public async Task<Appointment?> UpdateAppointmentPaymentStatus(int id, PaymentStatus status)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if( existingAppointment != null)
                existingAppointment.PaymentStatus = status;
            if (existingAppointment == null)
            {
                return null;
            }
            return await _appointmentRepository.UpdateAsync(existingAppointment);
        }
        public async Task<Appointment?> CancelAppointment(int id)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if(existingAppointment == null || existingAppointment.Status != AppointmentStatus.Boooked)
            {
                return null;
            }
            existingAppointment.Status = AppointmentStatus.Cancelled;
            var datebetween = DateOnly.FromDateTime(DateTime.Now).DayNumber -existingAppointment.Date.DayNumber ;
            if (datebetween > 7)
            {
                existingAppointment.RefundAmount = 0;
            }
            else
            {
                existingAppointment.RefundAmount = existingAppointment.TotalCost;
            } 
            existingAppointment.CancellationDate = DateOnly.FromDateTime(DateTime.Now);
            return await _appointmentRepository.UpdateAsync(existingAppointment);
        }
    }
}
