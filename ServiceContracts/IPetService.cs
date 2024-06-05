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
        Task<PetDTO?> AddPet(PetAddRequest petAddRequest);
        Task<PetDTO?> GetPetById(int id);
        Task<IEnumerable<PetDTO>> GetAllPets();
        Task<PetDTO?> UpdatePet(int id, PetUpdateRequest petUpdateRequest);
        Task<bool> RemovePetById(int id);
    }
}
