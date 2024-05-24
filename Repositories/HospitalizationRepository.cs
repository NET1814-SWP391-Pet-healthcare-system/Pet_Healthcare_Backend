using RepositoryContracts;
using Entities;

namespace Repositories
{
    public class HospitalizationRepository : IHospitalizationRepository
    {
        public bool Add(Hospitalization hospitalization)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hospitalization> GetAll()
        {
            throw new NotImplementedException();
        }

        public Hospitalization? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            if (_context.SaveChanges() == 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Hospitalization hospitalization)
        {
            throw new NotImplementedException();
        }
    }
}
