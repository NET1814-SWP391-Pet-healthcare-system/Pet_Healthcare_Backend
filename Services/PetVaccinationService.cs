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

        public async Task<PetVaccinationDTO> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest)
        {
            var petVaccination = petVaccinationAddRequest.ToPetVaccination();
            var isAdded = await _petVaccinationRepository.AddAsync(petVaccination);
            if(isAdded)
            {
                return petVaccination.ToPetVaccinationDto();
            }
            return null;
        }

        public async Task<IEnumerable<PetVaccinationDTO>> GetAllPetVaccinations()
        {
            var petVaccinations = await _petVaccinationRepository.GetAllAsync();
            return petVaccinations.Select(petVaccination => petVaccination.ToPetVaccinationDto());
        }

        public async Task<PetVaccinationDTO?> GetPetVaccinationById(int petId, int vaccineId)
        {
            var petVaccination = await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
            if(petVaccination == null)
            {
                return null;
            }
            return petVaccination.ToPetVaccinationDto();
        }

        public async Task<bool> RemovePetVaccination(int petId, int vaccineId)
        {
            var petVaccination = await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
            return await _petVaccinationRepository.Remove(petVaccination);
        }

        public async Task<PetVaccinationDTO?> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest)
        {
            var existingPetVaccination = await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
            if(existingPetVaccination == null)
            {
                return null;
            }
            var petVaccination = petVaccinationUpdateRequest.ToPetVaccination();
            var updatedPetVaccination = await _petVaccinationRepository.UpdateAsync(petId, vaccineId, petVaccination);
            return updatedPetVaccination.ToPetVaccinationDto();
        }
    }
}
