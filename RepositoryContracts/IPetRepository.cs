using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPetRepository
    {
        IEnumerable<Pet> GetAllPet();
        Pet? GetPetById(int id);
        bool AddPet(Pet pet);
        Pet UpdatePet(int id, Pet pet);
        bool RemovePet(Pet pet);
        bool SaveChanges();
    }
}
