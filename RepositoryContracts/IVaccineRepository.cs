using PetHealthCareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    internal interface IVaccineRepository
    {
        IEnumerable<Vaccine> GetAll();
        User GetById(int id);
        bool Add(Vaccine vaccine);
        bool Update(Vaccine vaccine);
        bool Remove(int id);
        bool SaveChanges();
    }
}
