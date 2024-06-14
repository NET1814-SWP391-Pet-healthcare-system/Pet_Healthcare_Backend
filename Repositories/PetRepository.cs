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

        public async Task<bool> AddPet(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllPet()
        {
            return await _context.Pets
                         .ToListAsync();
        }

        public async Task<Pet?> GetPetById(int id)
        {
            return await _context.Pets
                         .FirstOrDefaultAsync(p => p.PetId == id);
        }

        public async Task<bool> RemovePet(Pet pet)
        {
            _context.Pets.Remove(pet);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePet(Pet pet)
        {
            _context.Entry(pet).State = EntityState.Modified;
            await SaveChangesAsync();
            return true;
        }
    }
}
