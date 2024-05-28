using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RecordDTO
{
    public class RecordDto
    {
        public int? RecordId { get; set; }
        public string? PetName { get; set; }
        public int? NumberOfVisits { get; set; }
    }
}
