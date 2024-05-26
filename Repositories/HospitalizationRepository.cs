using RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class HospitalizationRepository : IHospitalizationRepository
    {
        private readonly ApplicationDbContext _context;
        public HospitalizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Hospitalization hospitalization)
        {
                if (hospitalization == null)
                {
                    return false;
                }
                _context.Hospitalizations.Add(hospitalization);
            SaveChanges();
            return true;
        }

        public IEnumerable<Hospitalization> GetAll()
        {
            return _context.Hospitalizations.ToList();
        }

        public Hospitalization? GetById(int id)
        {
            return _context.Hospitalizations.Find(id);
        }

        public bool Remove(int id)
        {
            if(GetById(id)==null)
            {
                return false;
            }
            _context.Hospitalizations.Remove(GetById(id));
            SaveChanges();
            return true;
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
                if (hospitalization == null)
                {
                    return false;
                }
                _context.Hospitalizations.Update(hospitalization);
            SaveChanges();
            return true;
        }
    }
}
