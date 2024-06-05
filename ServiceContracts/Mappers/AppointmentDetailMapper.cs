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
        public static AppointDetail ToAppointDetailDto(this AppointmentDetail request)
        {
            return new AppointDetail()
            {
                AppointmentDetailId = request.AppointmentDetailId,
                AppointmentId = request.AppointmentId,
                RecordId = request.RecordId,
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
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
                Diagnosis = request.Diagnosis,
                Treatment = request.Treatment,
                Medication = request.Medication
            };
        }
    }
}
