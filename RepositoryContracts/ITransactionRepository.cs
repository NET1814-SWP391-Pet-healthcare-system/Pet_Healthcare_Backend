using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetByUserIdAsync(string customerId);
        Task<Entities.Transaction?> GetByIdAsync(string id);
        Task<bool> AddAsync(Transaction transaction);
        Task<bool> UpdateAsync(Transaction transaction);
        Task<bool> RemoveAsync(Transaction transaction);
        Task<bool> SaveChangesAsync();
    }
}
