using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO.AppointmentDetailDTO
{
    public class AppointmentDetailAddRequest
    {
     
        [Required]
        public int? AppointmentId { get; set; }

        [Required]
        public int? RecordId { get; set; }
    
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medication { get; set; }

        public AppointmentDetail ToAppointmentDetail()
        {
            return new AppointmentDetail
            {
                
                AppointmentId = AppointmentId,
                RecordId = RecordId,
                Diagnosis = Diagnosis,
                Treatment = Treatment,
                Medication = Medication
            };
        }
    }

}
