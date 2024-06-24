using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IPetHealthTrackRepository
    {
        Task<IEnumerable<PetHealthTrack>> GetAllAsync();
        Task<PetHealthTrack?> GetByIdAsync(int id);
        Task<bool> AddAsync(PetHealthTrack? petHealthTrack);
        Task<bool> UpdateAsync(PetHealthTrack? petHealthTrack);
        Task<bool> RemoveAsync(PetHealthTrack petHealthTrack);
        Task<bool> SaveChangesAsync();
    }
}
