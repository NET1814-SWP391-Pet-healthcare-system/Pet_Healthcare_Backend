using Entities;
using RepositoryContracts;
using ServiceContracts;
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

        public async Task<PetVaccination> AddPetVaccination(PetVaccinationAddRequest petVaccinationAddRequest)
        {
            var request = petVaccinationAddRequest.ToPetVaccination();
            var isAdded = await _petVaccinationRepository.AddAsync(request);
            if(isAdded)
            {
                return request;
            }
            return null;
        }

        public async Task<IEnumerable<PetVaccination>> GetAllPetVaccinations()
        {
            return await _petVaccinationRepository.GetAllAsync();
        }

        public async Task<PetVaccination?> GetPetVaccinationById(int petId, int vaccineId)
        {
            return await _petVaccinationRepository.GetByIdAsync(petId, vaccineId);
        }

        public async Task<bool> RemovePetVaccination(int petId, int vaccineId)
        {
            var petVaccination = await GetPetVaccinationById(petId, vaccineId);
            return await _petVaccinationRepository.Remove(petVaccination);
        }

        public async Task<PetVaccination?> UpdatePetVaccination(int petId, int vaccineId, PetVaccinationUpdateRequest petVaccinationUpdateRequest)
        {
            var petVaccination = await GetPetVaccinationById(petId, vaccineId);
            if(petVaccination == null)
            {
                return null;
            }
            var request = petVaccinationUpdateRequest.ToPetVaccination();
            return await _petVaccinationRepository.UpdateAsync(petId, vaccineId, request);
        }
    }
}
