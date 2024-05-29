using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDetailDTO
{
    public class AppointDetailDto
    {
        public int AppointmentDetailId { get; set; }
        public int? AppointmentId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Medication { get; set; }
    }
}
