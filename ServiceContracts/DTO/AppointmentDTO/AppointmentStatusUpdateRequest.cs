using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentStatusUpdateRequest
    {
        public int Id { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
