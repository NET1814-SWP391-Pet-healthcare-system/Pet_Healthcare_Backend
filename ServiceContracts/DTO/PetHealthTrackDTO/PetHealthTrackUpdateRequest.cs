using Entities.Enum;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackUpdateRequest
    {
        [Required(ErrorMessage = "Must enter a valid ID")]
        public int PetHealthTrackId { get; set; }
        [Required]
        public int? HospitalizationId { get; set; }
        [Required]
        public Hospitalization? Hospitalization { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateOnly? DateOnly { get; set; }
        [Required]
        public PetStatus? Status { get; set; }

        public PetHealthTrack toPetHealthTrack()
        {
            return new PetHealthTrack
            {
                PetHealthTrackId = PetHealthTrackId,
                HospitalizationId = HospitalizationId,
                Hospitalization = Hospitalization,
                Description = Description,
                Date = DateOnly,
                Status = Status
            };
        }
    }
}
