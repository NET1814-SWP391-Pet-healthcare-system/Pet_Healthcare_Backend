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

        public bool AddPet(Pet pet)
        {
            _context.Pets.Add(pet);
            return SaveChanges();
        }

        public IEnumerable<Pet> GetAllPet()
        {
            return _context.Pets.Include(p => p.Customer).ToList();
        }

        public Pet? GetPetById(int id)
        {
            return _context.Pets.Include(p => p.Customer).FirstOrDefault(p => p.PetId == id);
        }

        public bool RemovePet(Pet pet)
        {
            _context.Pets.Remove(pet);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public Pet UpdatePet(int id, Pet pet)
        {
            var existingPet = GetPetById(id);
            Pet result = null;
            //update
            existingPet.CustomerId = pet.CustomerId;
            existingPet.Name = pet.Name;
            existingPet.Species = pet.Species;
            existingPet.Breed = pet.Breed;
            existingPet.Gender = pet.Gender;
            existingPet.Weight = pet.Weight;
            existingPet.ImageURL = pet.ImageURL;
            
            result = existingPet;
            SaveChanges();
            return result;
        }
    }
}
