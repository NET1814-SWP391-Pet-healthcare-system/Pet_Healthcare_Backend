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
    public class PetVaccinationRepository : IPetVaccinationRepository
    {
        private readonly ApplicationDbContext _context;

        public PetVaccinationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(PetVaccination petVaccination)
        {
            await _context.PetVaccinations.AddAsync(petVaccination);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<PetVaccination>> GetAllAsync()
        {
            var petVaccinationList = await _context.PetVaccinations
                .Include(pv => pv.Pet)
                .Include(pv => pv.Vaccine)
                .ToListAsync();
                
            return petVaccinationList;
        }

        public async Task<PetVaccination?> GetByIdAsync(int petId, int vaccineId)
        {
            return await _context.PetVaccinations
                .Include(pv => pv.Pet)
                .Include(pv => pv.Vaccine)
                .FirstOrDefaultAsync(pv => pv.PetId == petId && pv.VaccineId == vaccineId);
        }

        public async Task<bool> RemoveAsync(PetVaccination petVaccination)
        {
            _context.Remove(petVaccination);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(PetVaccination petVaccination)
        {
            _context.Entry(petVaccination).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
    }
}
