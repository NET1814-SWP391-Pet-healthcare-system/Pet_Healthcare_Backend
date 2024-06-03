using Entities.Enum;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackAddRequest
    {

        [Required]
        public int? HospitalizationId { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateOnly? DateOnly { get; set; }

        [Required]
        public PetStatus? Status { get; set; }

        public PetHealthTrack ToPetHealthTrack()
        {
            return new PetHealthTrack
            {
                HospitalizationId = HospitalizationId,
                Description = Description,
                Date = DateOnly,
                Status = Status
            };
        }
    }
}
