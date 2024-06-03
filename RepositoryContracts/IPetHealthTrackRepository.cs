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
        Task<PetHealthTrack?> AddAsync(PetHealthTrack? petHealthTrack);
        Task<PetHealthTrack?> UpdateAsync(PetHealthTrack? petHealthTrack);
        Task<PetHealthTrack>? RemoveAsync(int id);
    }
}
