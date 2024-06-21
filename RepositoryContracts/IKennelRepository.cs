using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IKennelRepository
    {
        Task<IEnumerable<Kennel>> GetAllAsync();
        Task<Kennel?> GetByIdAsync(int id);
        Task<bool> AddAsync(Kennel kennelModel);
        Task<bool> UpdateAsync(Kennel kennelModel);
        Task<bool> RemoveAsync(Kennel kennel);
        Task<bool> SaveChangesAsync();
    }
}
