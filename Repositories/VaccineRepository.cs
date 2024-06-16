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
    public class VaccineRepository : IVaccineRepository
    {
        private readonly ApplicationDbContext _context;

        public VaccineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Vaccine vaccine)
        {
            await _context.AddAsync(vaccine);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Vaccine>> GetAllAsync()
        {
            return await _context.Vaccines.Include(v => v.PetVaccinations).ToListAsync();
        }

        public async Task<Vaccine?> GetByIdAsync(int id)
        {
            return await _context.Vaccines.Include(v => v.PetVaccinations).FirstOrDefaultAsync(v => v.VaccineId == id);
        }

        public async Task<bool> RemoveAsync(Vaccine vaccine)
        {
            _context.Remove(vaccine);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Vaccine vaccine)
        {
            _context.Entry(vaccine).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
    }
}
