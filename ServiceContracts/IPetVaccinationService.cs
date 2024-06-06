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
        Task<PetVaccinationDTO?> GetPetVaccinationById(int petId, int vaccineId);
        Task<IEnumerable<PetVaccinationDTO>> GetAllPetVaccinations();
        Task<PetVaccinationDTO> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest);
        Task<PetVaccinationDTO?> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest);
        Task<bool> RemovePetVaccination(int petId, int vaccineId);
    }
}
