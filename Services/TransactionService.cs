using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
            
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            if (transaction == null)
            {
                return null;
            }
            await _transactionRepository.AddAsync(transaction);
            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var transactionList = await _transactionRepository.GetAllAsync();
            return transactionList;
        }

        public Task<Transaction?> GetByIdAsync(string id)
        {
            var transaction = _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                return null;
            }
            return transaction;
        }

        public Task<IEnumerable<Transaction>> GetByUserIdAsync(string userId)
        {
            var transaction = _transactionRepository.GetByUserIdAsync(userId);
            return transaction;
        }

        public async Task<Transaction> RemoveAsync(string id)
        {
            var transactionToRemove = await _transactionRepository.GetByIdAsync(id);
            await _transactionRepository.RemoveAsync(transactionToRemove);
            return transactionToRemove;
        }

        public async Task<Transaction> UpdateAsync(Entities.Transaction transaction)
        {
            var existingTransaction = await _transactionRepository.GetByIdAsync(transaction.TransactionId);
            existingTransaction.CustomerId = transaction.CustomerId;
            existingTransaction.AppointmentId = transaction.AppointmentId;

            await _transactionRepository.UpdateAsync(existingTransaction);
            return existingTransaction;

        }
    }
}
