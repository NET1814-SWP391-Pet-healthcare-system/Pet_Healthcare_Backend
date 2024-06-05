using Entities;
using ServiceContracts.DTO.PetDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Mappers
{
    public static class PetMapper
    {
        public static Pet ToPet(this PetAddRequest petAddRequest)
        {
            return new Pet()
            {
                Name = petAddRequest.Name,
                Species = petAddRequest.Species,
                Breed = petAddRequest.Breed,
                Gender = petAddRequest.Gender,
                Weight = petAddRequest.Weight,
                ImageURL = petAddRequest.ImageURL
            }; 
        }

        public static Pet ToPet(this PetUpdateRequest petUpdateRequest)
        {
            return new Pet()
            {
                //PetId = petUpdateRequest.PetId,
                //CustomerId = petUpdateRequest.CustomerId,
                //Name = petUpdateRequest.Name,
                //Species = petUpdateRequest.Species,
                //Breed = petUpdateRequest.Breed,
                //Gender = petUpdateRequest.Gender,
                //Weight = petUpdateRequest.Weight,
                //ImageURL = petUpdateRequest.ImageURL
            };
        }

        public static PetDTO ToPetDto(this Pet pet)
        {
            return new PetDTO()
            {
                Id = pet.PetId,
                CustomerId = pet.CustomerId,
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                Gender = pet.Gender,
                Weight = pet.Weight
            };
        }
    }
}
