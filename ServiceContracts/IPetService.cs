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
        Task<Pet?> AddPet(Pet pet);
        Task<Pet?> GetPetById(int id);
        Task<IEnumerable<Pet>> GetAllPets();
        Task<Pet?> UpdatePet(Pet pet);
        Task<bool> RemovePetById(int id);
    }
}
