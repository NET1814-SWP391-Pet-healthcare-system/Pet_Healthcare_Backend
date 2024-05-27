using RepositoryContracts;
using Entities;

namespace Repositories
{
    public class PetHealthTrackRepository : IPetHealthTrackRepository
    {
        private readonly ApplicationDbContext _context;
        public PetHealthTrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(PetHealthTrack? petHealthTrack)
        {
            if(petHealthTrack == null)
                return false;
            _context.PetHealthTracks.Add(petHealthTrack);
            return SaveChanges();

        }

        public IEnumerable<PetHealthTrack> GetAll()
        {
            return _context.PetHealthTracks.ToList();
        }

        public PetHealthTrack? GetById(int id)
        {
            return _context.PetHealthTracks.Find(id);
        }

        public bool Remove(int id)
        {
            var pht = _context.PetHealthTracks.Find(id);
            if (pht == null) return false;
            _context.PetHealthTracks.Remove(pht);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(PetHealthTrack? petHealthTrack)
        {
            if (petHealthTrack == null) return false;
            _context.PetHealthTracks.Update(petHealthTrack);
            return SaveChanges();
        }   
    }
}
