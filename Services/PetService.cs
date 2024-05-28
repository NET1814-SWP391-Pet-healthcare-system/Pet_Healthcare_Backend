﻿using Entities;
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
        private readonly IUserRepository _userRepository;

        public PetService(IPetRepository petRepository, IUserRepository userRepository)
        {
            _petRepository = petRepository;
            _userRepository = userRepository;
        }

        public Pet? AddPet(PetAddRequest petAddRequest)
        {
            var pet = petAddRequest.ToPet();
            pet.Customer = _userRepository.GetUserById(petAddRequest.CustomerId) as Customer;
            var isAdded = _petRepository.AddPet(pet);
            if(isAdded)
            {
                return pet;
            }
            return null;

        }

        public Pet? GetPetById(int id)
        {
            return _petRepository.GetPetById(id);
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _petRepository.GetAllPet();
        }

        public Pet? UpdatePet(int id, PetUpdateRequest petUpdateRequest)
        {
            var pet = _petRepository.GetPetById(id);
            if(pet == null)
            {
                return null; 
            }
            var request = petUpdateRequest.ToPet();
            request.Customer = _userRepository.GetUserById(petUpdateRequest.CustomerId) as Customer;
            return _petRepository.UpdatePet(id, request);
        }

        public bool RemovePetById(int id)
        {
            var pet = GetPetById(id);
            return _petRepository.RemovePet(pet);
        }

    }
}