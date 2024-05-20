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
        IEnumerable<Pet> GetAll();
        Pet GetById(int id);
        bool Add(Pet pet);
        bool Update(Pet pet);
        bool Remove(int id);
        bool SaveChanges();
    }
}
