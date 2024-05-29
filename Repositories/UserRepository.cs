using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomerWithPetsAsync(string customerId)
        {
            return await _context.Customers.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Id == customerId);
        }
    }
}
