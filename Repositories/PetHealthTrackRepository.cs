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

        public async Task<PetHealthTrack>? AddAsync(PetHealthTrack? petHealthTrack)
        {
            await _context.PetHealthTracks.AddAsync(petHealthTrack);
            await _context.SaveChangesAsync();
            return petHealthTrack;
        }

        public async Task<IEnumerable<PetHealthTrack>> GetAllAsync()
        {
            return await _context.PetHealthTracks.ToListAsync();
        }

        public async Task<PetHealthTrack>? GetByIdAsync(int id)
        {
            return _context.PetHealthTracks.FirstOrDefault(x => x.PetHealthTrackId == id);
        }

        public async Task<PetHealthTrack>? RemoveAsync(int id)
        {
            var pht = await GetByIdAsync(id);
            if (pht == null)
            {
                return null;
            }
            _context.Remove(pht);
            await _context.SaveChangesAsync();
            return pht;
        }

        public async Task<PetHealthTrack>? UpdateAsync(PetHealthTrack? petHealthTrack)
        {
            if (petHealthTrack == null)
            {
                return null;
            }
            var existingpetHealthTrack = await GetByIdAsync(petHealthTrack.PetHealthTrackId);
            if (existingpetHealthTrack == null)
            {
                return null;
            }
            _context.Update(petHealthTrack);
            await _context.SaveChangesAsync();
            return existingpetHealthTrack;
        }
    }
}
