using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IVaccineRepository
    {
        IEnumerable<Vaccine> GetAll();
        Vaccine? GetById(int id);
        bool Add(Vaccine vaccine);
        bool Update(Vaccine vaccine);
        bool Remove(int id);
        bool SaveChanges();
    }
}
