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
        
        public async Task<IEnumerable<Vet>?> GetAvailableVetsAsync(DateOnly date, int slotId)
        {
            // Get all vets
            var vets = await _context.Vets.ToListAsync();

            // Get all appointments in date and slot
            var appointments = await _context.Appointments
                .Where(a => a.Date == date && a.SlotId == slotId)
                .ToListAsync();

            // Create a list of available vets
            var availableVets = new List<Vet>();

            // Get first vet that is not in a appointment in the given slot
            foreach (var vet in vets)
            {
                if (!appointments.Any(a => a.VetId == vet.Id))
                {
                    availableVets.Add(vet);
                }
            }

            if (availableVets.Count == 0)
            {
                return null;
            }

            return availableVets;
        }
        public async Task<Vet?> GetVetById(string id)
        {
            return await _context.Vets.FindAsync(id);
        }
    }
}
