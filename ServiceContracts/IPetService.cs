using Entities;
using ServiceContracts.DTO.PetDTO;
using ServiceContracts.DTO.PetHealthTrackDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPetService
    {
        Pet? AddPet(PetAddRequest petAddRequest);
        Pet? GetPetById(int id);
        IEnumerable<Pet> GetAllPets();
        Pet? UpdatePet(int id, PetUpdateRequest petUpdateRequest);
        bool RemovePetById(int id);
    }
}
