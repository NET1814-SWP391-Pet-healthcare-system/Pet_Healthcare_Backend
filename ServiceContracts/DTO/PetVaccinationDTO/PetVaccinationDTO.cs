using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.PetVaccinationDTO
{
    public class PetVaccinationDTO
    {
        public int? PetId { get; set; }
        public string? PetName { get; set; }
        public int? VaccineId { get; set; }
        public string? VaccineName { get; set; }
        public DateOnly? VaccinationDate { get; set; }
    }
}
