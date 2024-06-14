using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllPet();
        Task<Pet?> GetPetById(int id);
        Task<bool> AddPet(Pet pet);
        Task<bool> UpdatePet(Pet pet);
        Task<bool> RemovePet(Pet pet);
        Task<bool> SaveChangesAsync();
    }
}
