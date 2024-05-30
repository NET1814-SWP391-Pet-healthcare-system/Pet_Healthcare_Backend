using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task<Pet?> AddPet(PetAddRequest petAddRequest)
        {
            var request = petAddRequest.ToPet();
            var isAdded = await _petRepository.AddPet(request);
            if (isAdded)
            {
                return request;
            }
            return null;

        }

        public async Task<Pet?> GetPetById(int id)
        {
            return await _petRepository.GetPetById(id);
        }

        public async Task<IEnumerable<Pet>> GetAllPets()
        {
            return await _petRepository.GetAllPet();
        }

        public async Task<Pet?> UpdatePet(int id, PetUpdateRequest petUpdateRequest)
        {
            var pet = await _petRepository.GetPetById(id);
            if (pet == null)
            {
                return null;
            }
            var request = petUpdateRequest.ToPet();
            return await _petRepository.UpdatePet(id, request);
        }

        public async Task<bool> RemovePetById(int id)
        {
            var pet = await GetPetById(id);
            return await _petRepository.RemovePet(pet);
        }

    }
}
