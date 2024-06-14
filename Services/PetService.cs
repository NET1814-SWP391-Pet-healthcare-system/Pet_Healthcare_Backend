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
        private readonly UserManager<User> _userManager;

        public PetService(IPetRepository petRepository, UserManager<User> userManager)
        {
            _petRepository = petRepository;
            _userManager = userManager;
        }

        public async Task<PetDTO?> AddPet(Pet pet)
        {
            if(pet == null)
                return null;

            await _petRepository.AddPet(pet);
            return pet.ToPetDto();
        }

        public async Task<PetDTO?> GetPetById(int id)
        {
            var pet = await _petRepository.GetPetById(id);
            if (pet == null)
            {
                return null;
            }
            return pet.ToPetDto();
        }

        public async Task<IEnumerable<PetDTO>> GetAllPets()
        {
            var petList = await _petRepository.GetAllPet();
            return petList.Select(pet => pet.ToPetDto());
        }

        public async Task<PetDTO?> UpdatePet(Pet pet)
        {
            if (pet == null)
            {
                return null;
            }
            var existingPet = await _petRepository.GetPetById(pet.PetId);
            existingPet.Name = string.IsNullOrEmpty(pet.Name) ? existingPet.Name : pet.Name;
            existingPet.Species = string.IsNullOrEmpty(pet.Species) ? existingPet.Species : pet.Species;
            existingPet.Breed = string.IsNullOrEmpty(pet.Breed) ? existingPet.Breed : pet.Breed;
            existingPet.Gender = pet.Gender == null ? existingPet.Gender : pet.Gender;
            existingPet.Weight = pet.Weight == null ? existingPet.Weight : pet.Weight;
            existingPet.ImageURL = string.IsNullOrEmpty(pet.ImageURL) ? existingPet.ImageURL : pet.ImageURL;

            await _petRepository.UpdatePet(existingPet);
            return existingPet.ToPetDto();
        }

        public async Task<bool> RemovePetById(int id)
        {
            var pet = await _petRepository.GetPetById(id);
            return await _petRepository.RemovePet(pet);
        }
            
    }
}
