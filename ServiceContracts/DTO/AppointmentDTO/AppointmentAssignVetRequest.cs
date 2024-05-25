using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.AppointmentDTO
{
    public class AppointmentAssignVetRequest
    {
        public Appointment ToAppointment(int vetId)
        {
            return new Appointment() 
            { 
                VetId = vetId,
            };
        }
    }
}
