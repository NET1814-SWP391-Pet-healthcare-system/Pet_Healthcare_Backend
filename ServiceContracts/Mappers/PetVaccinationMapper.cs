using Entities;
using ServiceContracts.DTO.PetVaccinationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class PetVaccinationMapper
    {
        public static PetVaccination ToPetVaccination(this PetVaccinationAddRequest petVaccinationAddRequest)
        {
            return new PetVaccination()
            {
                PetId = petVaccinationAddRequest.PetId,
                VaccineId = petVaccinationAddRequest.VaccineId,
                VaccinationDate = petVaccinationAddRequest.VaccinationDate
            };
        }

        public static PetVaccination ToPetVaccination(this PetVaccinationUpdateRequest petVaccinationUpdateRequest)
        {
            return new PetVaccination()
            {
                VaccinationDate = petVaccinationUpdateRequest.VaccinationDate
            };
        }

        public static PetVaccinationDTO ToPetVaccinationDto(this PetVaccination petVaccination)
        {
            return new PetVaccinationDTO
            {
                PetId = petVaccination.PetId,
                VaccineId = petVaccination.VaccineId,
                VaccinationDate = petVaccination.VaccinationDate
            };
        }
    }
}
