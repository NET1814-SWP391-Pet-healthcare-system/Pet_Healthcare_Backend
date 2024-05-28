using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetVaccinationDTO
{
    public class PetVaccinationUpdateRequest
    {
        [Required(ErrorMessage = "PetId is required")]
        public int PetId { get; set; }
        [Required(ErrorMessage = "VaccineId is required")]
        public int VaccineId { get; set; }
        public DateOnly? VaccinationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
