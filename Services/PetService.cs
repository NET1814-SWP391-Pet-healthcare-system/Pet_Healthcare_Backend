using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<Pet?> AddPet(Pet pet)
        {
            if(pet is null)
                return null;

            await _petRepository.AddAsync(pet);
            return pet;
        }

        public async Task<Pet?> GetPetById(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return null;
            }
            return pet;
        }

        public async Task<IEnumerable<Pet>> GetAllPets()
        {
            return await _petRepository.GetAllAsync();
        }

        public async Task<Pet?> UpdatePet(Pet pet)
        {
            if (pet == null)
            {
                return null;
            }
            var existingPet = await _petRepository.GetByIdAsync(pet.PetId);
            existingPet.Name = string.IsNullOrEmpty(pet.Name) ? existingPet.Name : pet.Name;
            existingPet.Species = string.IsNullOrEmpty(pet.Species) ? existingPet.Species : pet.Species;
            existingPet.Breed = string.IsNullOrEmpty(pet.Breed) ? existingPet.Breed : pet.Breed;
            existingPet.Gender = pet.Gender == null ? existingPet.Gender : pet.Gender;
            existingPet.Weight = pet.Weight == null ? existingPet.Weight : pet.Weight;
            existingPet.ImageURL = string.IsNullOrEmpty(pet.ImageURL) ? existingPet.ImageURL : pet.ImageURL;

            await _petRepository.UpdateAsync(existingPet);
            return existingPet;
        }

        public async Task<bool> RemovePetById(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return await _petRepository.RemoveAsync(pet);
        }

        public async Task<IEnumerable<Pet>> GetCustomerPet(string customerId)
        {
            var pets = await _petRepository.GetAllAsync();
            var customerPets = pets.Where(p => p.CustomerId.Equals(customerId));
            return customerPets;
        }
            
    }
}
