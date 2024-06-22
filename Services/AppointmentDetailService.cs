﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories;
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

        public async Task<AppointmentDetail> AddAppointmentDetailAsync(AppointmentDetail? request)
        {
            if (request == null)
            {
                return null;
            }
            await _appointmentDetailRepository.AddAsync(request);
            return request;
        }

 

        public async Task<AppointmentDetail?> GetAppointmentDetailByIdAsync(int id)
        {
            
            var appoint = await _appointmentDetailRepository.GetByIdAsync(id);
            if (appoint == null)
            {
                return null;
            }
            return appoint;
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAppointmentDetailsAsync()
        {
            return await _appointmentDetailRepository.GetAllAsync();
        }

        public async Task<bool> RemoveAppointmentDetailAsync(int id)
        {
            var appointDetail = await _appointmentDetailRepository.GetByIdAsync(id);
            if (appointDetail == null)
            {
                return false;
            }
            return await _appointmentDetailRepository.RemoveAsync(appointDetail);
        }


        public async Task<AppointmentDetail?> UpdateAppointmentDetailAsync(AppointmentDetail request)
        {
            var ExistingAppointmentDetail = await _appointmentDetailRepository.GetByIdAsync(request.AppointmentDetailId);
            if (request == null || ExistingAppointmentDetail==null)
            {
                return null;
            }
            ExistingAppointmentDetail.Treatment = request.Treatment ==null ? ExistingAppointmentDetail.Treatment : request.Treatment;
            ExistingAppointmentDetail.Diagnosis = request.Diagnosis == null ? ExistingAppointmentDetail.Diagnosis : request.Diagnosis;
            ExistingAppointmentDetail.Record = request.Record == null ? ExistingAppointmentDetail.Record : request.Record;
            ExistingAppointmentDetail.Medication = request.Medication;
            await _appointmentDetailRepository.UpdateAsync(ExistingAppointmentDetail);
            return ExistingAppointmentDetail;
        }

 
    }
}
