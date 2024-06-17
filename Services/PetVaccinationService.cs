using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.PetVaccinationDTO;
using ServiceContracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PetVaccinationService : IPetVaccinationService
    {
        private readonly IPetVaccinationRepository _petVaccinationRepository;

        public PetVaccinationService(IPetVaccinationRepository petVaccinationRepository)
        {
            _petVaccinationRepository = petVaccinationRepository;
        }

        public async Task<PetVaccination> AddPetVaccination(PetVaccination petVaccination)
        {
            if(petVaccination is null){
                return null;
            }
            await _petVaccinationRepository.AddAsync(petVaccination);
            return petVaccination;
        }

        public async Task<IEnumerable<PetVaccination>> GetAllPetVaccinations()
        {
            return await _petVaccinationRepository.GetAllAsync();
        }

        public async Task<PetVaccination?> GetPetVaccinationById(int petId, int vaccineId)
        {
            var petVaccination = await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
            if(petVaccination == null)
            {
                return null;
            }
            return petVaccination;
        }

        public async Task<bool> RemovePetVaccination(int petId, int vaccineId)
        {
            var petVaccination = await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
            return await _petVaccinationRepository.RemoveAsync(petVaccination);
        }

        public async Task<PetVaccination?> UpdatePetVaccination(PetVaccination petVaccination)
        {
            if(petVaccination is null)
            {
                return null;    
            }
            var existingPetVaccination = await _petVaccinationRepository.GetByIdAsync(petVaccination.PetId, petVaccination.VaccineId);
            existingPetVaccination.VaccinationDate = petVaccination.VaccinationDate;
            await _petVaccinationRepository.UpdateAsync(existingPetVaccination);
            return existingPetVaccination;
        }
    }
}
