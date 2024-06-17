using Entities;
using ServiceContracts.DTO.VaccineDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class VaccineMapper
    {
        public static Vaccine ToVaccineFromAdd(this VaccineAddRequest vaccineAddRequest)
        {
            return new Vaccine()
            {
                Name = vaccineAddRequest.Name,
                Description = vaccineAddRequest.Description,
                IsAnnualVaccine = vaccineAddRequest.IsAnnualVaccine,
            };
        }

        public static Vaccine ToVaccineFromUpdate(this VaccineUpdateRequest vaccineUpdateRequest)
        {
            return new Vaccine()
            {
                Name = vaccineUpdateRequest.Name,
                Description = vaccineUpdateRequest.Description,
                IsAnnualVaccine = vaccineUpdateRequest.IsAnnualVaccine,
            };
        }

        public static VaccineDTO ToVaccineDto(this Vaccine vaccine)
        {
            return new VaccineDTO()
            {
                VaccineId = vaccine.VaccineId,
                Name = vaccine.Name,
                Description = vaccine.Description,
                IsAnnualVaccine = vaccine.IsAnnualVaccine
            };
        }
    }
}
