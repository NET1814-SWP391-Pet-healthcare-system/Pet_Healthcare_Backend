using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RecordDTO
{
    public class RecordUpdateRequest
    {
        public int PetId { get; set; }
        public int AppointmentDetaId { get; set; }
        public int NumberOfVisits { get; set; }
    }
}
