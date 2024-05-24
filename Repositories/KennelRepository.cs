using RepositoryContracts;
using Entities;

namespace Repositories
{
    public class KennelRepository : IKennelRepository
    {
        private readonly ApplicationDbContext _context;
        public KennelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Kennel? kennel)
        {
            if (kennel == null)
            {
                return false;
            }
            _context.Kennels.Add(kennel);
            return SaveChanges();
        }

        public IEnumerable<Kennel> GetAll()
        {
            return _context.Kennels.ToList();
        }

        public Kennel? GetById(int id)
        {
            return _context.Kennels.Find(id);
        }

        public bool Remove(int id)
        {
            var kennel = _context.Kennels.Find(id);
            if (kennel == null) return false;
            _context.Kennels.Remove(kennel);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Kennel? kennel)
        {
            if (kennel == null) return false;
            _context.Kennels.Update(kennel);
            return SaveChanges();
        }
    }
}
