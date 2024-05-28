using Entities;
using ServiceContracts.DTO.PetVaccinationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPetVaccinationService
    {
        Task<PetVaccination> GetPetVaccinationById(int petId, int vaccineId);
        Task<IEnumerable<PetVaccination>> GetAllPetVaccinations();
        Task<PetVaccination> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest);
        Task<PetVaccination?> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest);
        Task<bool> RemovePetVaccination(int petId, int vaccineId);
    }
}
