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

        public async Task<Customer?> GetCustomerWithPetsAsync(string customerId)
        {
            return await _context.Customers.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<Vet?> GetAvailableVetAsync(DateOnly date, int slotId)
        {
            // Get all vets
            var vets = await _context.Vets.ToListAsync();

            // Get all appointments in date and slot
            var appointments = await _context.Appointments
                .Where(a => a.Date == date && a.SlotId == slotId)
                .ToListAsync();

            // Get first vet that is not in a appointment in the given slot
            foreach (var vet in vets)
            {
                if (!appointments.Any(a => a.VetId == vet.Id))
                {
                    return vet;
                }
            }

            return null;
        }
    }
}
