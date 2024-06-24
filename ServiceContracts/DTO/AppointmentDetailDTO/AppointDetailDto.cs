using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDetailDTO
{
    public class AppointDetailDTO
    {
        public int AppointmentDetailId { get; set; }
        public int? AppointmentId { get; set; }
        public int? RecordId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medication { get; set; }
        public string? VetName { get; set; }
        public string? Service { get; set; }
        public DateOnly? Date { get; set; }
    }
}
