using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDetailDTO
{
    public class AppointmentDetailUpdateRequest
    {
        [Required]
        public int AppointmentDetailId { get; set; }
        public int? AppointmentId { get; set; }
        public int? RecordId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medication { get; set; }
        
        public AppointmentDetail UpdateAppointmentDetail(AppointmentDetail appointmentDetail)
        {
            return new AppointmentDetail
            {
                AppointmentDetailId = AppointmentDetailId,
                AppointmentId = AppointmentId,
                RecordId = RecordId,
                Diagnosis = Diagnosis,
                Treatment = Treatment,
                Medication = Medication
            };
        }
    }
}
