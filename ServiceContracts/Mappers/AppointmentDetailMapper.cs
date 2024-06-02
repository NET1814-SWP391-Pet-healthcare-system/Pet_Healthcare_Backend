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
        public static AppointDetailDto ToAppointDetailDto(this AppointmentDetail request)
        {
            return new AppointDetailDto
            {
                AppointmentDetailId = request.AppointmentDetailId,
                AppointmentId = request.AppointmentId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
            };
        }
        public static AppointmentDetail ToAppointmentDetailFromAdd(this AppointmentDetailAddRequest request)
        {
            return new AppointmentDetail
            {
                AppointmentId = request.AppointmentId,
                RecordId = request.RecordId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
            };
        }
        public static AppointmentDetail ToAppointmentDetailUpdateDiagnosis(this AppointmentDetailUpdateDiagnosis request)
        {
            return new AppointmentDetail
            {
                AppointmentDetailId = request.AppointmentDetailId,
                AppointmentId = request.AppointmentId,
                RecordId = request.RecordId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
            };
        }
    }
}
