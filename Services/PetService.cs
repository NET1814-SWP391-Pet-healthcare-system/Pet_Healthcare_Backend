using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserManager<User> _userManager;

        public PetService(IPetRepository petRepository, UserManager<User> userManager)
        {
            _petRepository = petRepository;
            _userManager = userManager;
        }

        public async Task<PetDTO?> AddPet(PetAddRequest petAddRequest)
        {
            var pet = petAddRequest.ToPet();
            var customer = await _userManager.FindByNameAsync(petAddRequest.CustomerUsername);
            pet.CustomerId = customer.Id;
            var isAdded = await _petRepository.AddPet(pet);
            if (isAdded)
            {
                return pet.ToPetDto();
            }
            return null;

        }

        public async Task<PetDTO?> GetPetById(int id)
        {
            var pet = await _petRepository.GetPetById(id);
            return pet.ToPetDto();
        }

        public async Task<IEnumerable<PetDTO>> GetAllPets()
        {
            var petList = await _petRepository.GetAllPet();
            List<PetDTO> result = new List<PetDTO>();
            foreach (var pet in petList)
            {
                result.Add(pet.ToPetDto());
            }
            return result;
        }

        public async Task<PetDTO?> UpdatePet(int id, PetUpdateRequest petUpdateRequest)
        {
            var existingPet = await _petRepository.GetPetById(id);
            if (existingPet == null)
            {
                return null;
            }
            var pet = petUpdateRequest.ToPet();
            var updatedPet = await _petRepository.UpdatePet(id, pet);
            return updatedPet.ToPetDto();
        }

        public async Task<bool> RemovePetById(int id)
        {
            var pet = await _petRepository.GetPetById(id);
            return await _petRepository.RemovePet(pet);
        }

    }
}
