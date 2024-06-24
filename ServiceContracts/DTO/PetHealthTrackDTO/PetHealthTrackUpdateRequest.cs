using Entities;
using Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.PetHealthTrackDTO
{
    public class PetHealthTrackUpdateRequest
    {
        [Required]
        public int? HospitalizationId { get; set; } 
        public string? Description { get; set; }

        [Required]
        public DateOnly? DateOnly { get; set; }

        [Required]
        public PetStatus? Status { get; set; }

    }
}
