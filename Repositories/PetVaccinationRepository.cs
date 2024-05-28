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
            return await _context.PetVaccinations
                .Include(pv => pv.Pet)
                .Include(pv => pv.Vaccine)
                .ToListAsync();
        }

        public async Task<PetVaccination?> GetByIdAsync(int petId, int vaccineId)
        {
            return await _context.PetVaccinations
                .Include(pv => pv.Pet)
                .Include(pv => pv.Vaccine)
                .FirstOrDefaultAsync(pv => pv.PetId == petId && pv.VaccineId == vaccineId);
        }

        public async Task<bool> Remove(PetVaccination petVaccination)
        {
            _context.Remove(petVaccination);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PetVaccination> UpdateAsync(int petId, int vaccineId, PetVaccination petVaccination)
        {
            var existingPetVaccination = await GetByIdAsync(petId, vaccineId);
            existingPetVaccination.VaccinationDate = petVaccination.VaccinationDate;
            await SaveChangesAsync();
            return existingPetVaccination;
        }
    }
}
