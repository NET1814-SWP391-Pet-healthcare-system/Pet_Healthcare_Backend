using PetHealthCareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface ISlotRepository
    {
        IEnumerable<Slot> GetAll();
        Slot GetById(int id);
        bool Add(Slot slot);
        bool Update(Slot slot);
        bool Remove(int id);
        bool SaveChanges();
    }
}
