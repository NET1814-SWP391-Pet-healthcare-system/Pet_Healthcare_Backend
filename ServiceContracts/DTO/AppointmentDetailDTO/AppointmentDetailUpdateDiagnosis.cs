using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDetailDTO
{
    public class AppointmentDetailUpdateDiagnosis
    {
        [Required (ErrorMessage = "Please fill the Record ID")]
        public int? RecordId { get; set; }
        [Required (ErrorMessage = "Please fill the Diagnosis")]
        public string? Diagnosis { get; set; }
        [Required (ErrorMessage = "Please fill the Treatment")]
        public string? Treatment { get; set; }
        [Required (ErrorMessage = "Please fill the Medication")]
        public string? Medication { get; set; }
        
    }
}
