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
        Task<Kennel> AddAsync(Kennel kennelModel);
        Task<Kennel?> UpdateAsync(Kennel kennelModel);
        Task<Kennel?> RemoveAsync(int id);
    }
}
