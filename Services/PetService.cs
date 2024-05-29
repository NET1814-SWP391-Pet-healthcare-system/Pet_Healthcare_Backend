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

        public Pet? AddPet(PetAddRequest petAddRequest)
        {
            var request = petAddRequest.ToPet();
            var isAdded = _petRepository.AddPet(request);
            if(isAdded)
            {
                return request;
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
            return _petRepository.UpdatePet(id, request);
        }

        public bool RemovePetById(int id)
        {
            var pet = GetPetById(id);
            return _petRepository.RemovePet(pet);
        }

    }
}
