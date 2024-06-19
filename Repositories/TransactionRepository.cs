using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(string id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(string customerId)
        {
            var transaction =  _context.Transactions.Where(t => t.CustomerId == customerId);
            return transaction.ToList();
        }

        public async Task<bool> RemoveAsync(Transaction transaction)
        {
            _context.Remove(transaction);
            return await SaveChangesAsync();
        }

        public  async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
    }
}
