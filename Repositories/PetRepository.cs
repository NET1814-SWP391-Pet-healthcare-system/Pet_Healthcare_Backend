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

        public async Task<Pet> UpdatePet(int id, Pet pet)
        {
            var existingPet = await GetPetById(id);
            Pet result = null;
            //update
            existingPet.Name = pet.Name;
            existingPet.Species = pet.Species;
            existingPet.Breed = pet.Breed;
            existingPet.Gender = pet.Gender;
            existingPet.Weight = pet.Weight;
            existingPet.ImageURL = pet.ImageURL;

            result = existingPet;
            await SaveChangesAsync();
            return result;
        }
    }
}
