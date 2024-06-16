using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _context.Pets
                         .ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(int id)
        {
            return await _context.Pets
                         .FirstOrDefaultAsync(p => p.PetId == id);
        }

        public async Task<bool> RemoveAsync(Pet pet)
        {
            _context.Pets.Remove(pet);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Pet pet)
        {
            _context.Entry(pet).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
    }
}
