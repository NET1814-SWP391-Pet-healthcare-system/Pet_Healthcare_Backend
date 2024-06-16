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
        public DateOnly? VaccinationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
