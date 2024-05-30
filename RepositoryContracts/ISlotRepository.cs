using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface ISlotRepository
    {
        Task<IEnumerable<Slot>> GetAllAsync();
        Task<Slot?> GetByIdAsync(int id);
        Task<Slot> AddAsync(Slot slotModel);
        Task<Slot?> UpdateAsync(Slot slotModel);
        Task<Slot?> RemoveAsync(int id);
    }
}
