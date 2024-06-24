using RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class PetHealthTrackRepository : IPetHealthTrackRepository
    {
        private readonly ApplicationDbContext _context;
        public PetHealthTrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(PetHealthTrack? petHealthTrack)
        {
            await _context.PetHealthTracks.AddAsync(petHealthTrack);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<PetHealthTrack>> GetAllAsync()
        {
            return await _context.PetHealthTracks
                .Include(x => x.Hospitalization)
                .Include(x => x.Hospitalization.Pet)
                .Include(x => x.Hospitalization.Vet)
                .ToListAsync();
        }

        public async Task<PetHealthTrack?> GetByIdAsync(int id)
        {
            return await _context.PetHealthTracks
                .Include(x => x.Hospitalization)
                .Include(x => x.Hospitalization.Pet)
                .Include(x => x.Hospitalization.Vet)
                .FirstOrDefaultAsync(x => x.PetHealthTrackId == id);
        }

        public async Task<bool> RemoveAsync(PetHealthTrack pht)
        {
            _context.Remove(pht);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(PetHealthTrack petHealthTrack)
        {
            _context.Entry(petHealthTrack).State = EntityState.Modified;
            return await SaveChangesAsync();

        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
