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
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(int id);
        Task<bool> AddAsync(Pet pet);
        Task<bool> UpdateAsync(Pet pet);
        Task<bool> RemoveAsync(Pet pet);
        Task<bool> SaveChangesAsync();
    }
}
