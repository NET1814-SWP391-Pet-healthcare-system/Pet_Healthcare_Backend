using Entities;
using ServiceContracts.DTO.AppointmentDetailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class AppointmentDetailMapper
    {
        public static AppointDetailDTO ToAppointDetailDto(this AppointmentDetail request)
        {
            return new AppointDetailDTO()
            {
                AppointmentDetailId = request.AppointmentDetailId,
                AppointmentId = request.AppointmentId,
                RecordId = request.RecordId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication,
                VetName = request.Appointment?.Vet?.UserName,
                Service = request.Appointment?.Service?.Name,
                Date = request.Appointment?.Date
            };
        }
        public static AppointmentDetail ToAppointmentDetailFromAdd(this AppointmentDetailAddRequest? appointmentDetail)
        {
            return new AppointmentDetail()
            { 
                AppointmentId = appointmentDetail.AppointmentId,
                Diagnosis = appointmentDetail.Diagnosis,
                Treatment = appointmentDetail.Treatment,
                Medication = appointmentDetail.Medication
            };
        }
        public static AppointmentDetail ToAppointmentDetailUpdateDiagnosis(this AppointmentDetailUpdateDiagnosis request)
        {
            return new AppointmentDetail()
            {
                RecordId = request.RecordId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
            };
        }
    }
}
