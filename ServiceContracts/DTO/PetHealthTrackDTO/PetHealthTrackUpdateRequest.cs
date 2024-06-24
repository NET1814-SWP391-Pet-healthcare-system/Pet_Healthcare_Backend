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
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "The date must be in the format YYYY-MM-DD.")]
        public string? Date { get; set; }

        [Required]
        public PetStatus? Status { get; set; }

    }
}
