using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IHospitalizationRepository
    {
        IEnumerable<Hospitalization> GetAll();
        Pet? GetById(int id);
        bool Add(Hospitalization hospitalization);
        bool Update(Hospitalization hospitalization);
        bool Remove(int id);
        bool SaveChanges();
    }
}
