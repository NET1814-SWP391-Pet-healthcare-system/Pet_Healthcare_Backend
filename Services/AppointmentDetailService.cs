using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.AppointmentDetailDTO;
using ServiceContracts.Mappers;

namespace Services
{
    public class AppointmentDetailService : IAppointmentDetailService
    {
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;

        public AppointmentDetailService(IAppointmentDetailRepository appointmentDetailRepository)
        {
            _appointmentDetailRepository = appointmentDetailRepository;
        }

        public async Task<AppointmentDetail> AddAppointmentDetail(AppointmentDetail? request)
        {
            return await _appointmentDetailRepository.AddAsync(request);
        }

 

        public async Task<AppointmentDetail?>  GetAppointmentDetailById(int id)
        {
            return await _appointmentDetailRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAppointmentDetails()
        {
            return await _appointmentDetailRepository.GetAllAsync();
        }

        public async Task<AppointmentDetail> RemoveAppointmentDetail(int id)
        {
             return await _appointmentDetailRepository.RemoveAsync(id);
        }

        public async Task<AppointmentDetail> UpdateAppointmentDetail(int i ,AppointmentDetail request)
        {
            var ExistingAppointmentDetail = await _appointmentDetailRepository.GetByIdAsync(i);
            if(request == null || request == null || ExistingAppointmentDetail==null)
            {
                return null;
            }
            var AppointmentDetail = request;
            ExistingAppointmentDetail.Appointment = request.Appointment;
            ExistingAppointmentDetail.Treatment = request.Treatment;
            ExistingAppointmentDetail.Diagnosis = request.Diagnosis;
            ExistingAppointmentDetail.Record = request.Record;
            ExistingAppointmentDetail.Medication = request.Medication;
            _appointmentDetailRepository.UpdateAsync(ExistingAppointmentDetail);
            return ExistingAppointmentDetail;
        }

 
    }
}
