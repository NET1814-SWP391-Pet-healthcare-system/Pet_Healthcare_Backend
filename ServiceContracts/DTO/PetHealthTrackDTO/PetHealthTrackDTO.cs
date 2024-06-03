using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackDTO
    {
        public int PetHealthTrackId { get; set; }
        public int? HospitalizationId { get; set; }
        public string? Description { get; set; }
        public DateOnly? DateOnly { get; set; }
        public PetStatus? Status { get; set; }
    }
}
