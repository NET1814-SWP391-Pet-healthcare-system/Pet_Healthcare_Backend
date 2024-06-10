using RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class KennelRepository : IKennelRepository
    {
        private readonly ApplicationDbContext _context;
        public KennelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Kennel> AddAsync(Kennel kennelModel)
        {
            await _context.Kennels.AddAsync(kennelModel);
            await _context.SaveChangesAsync();
            return kennelModel;
        }

        public async Task<IEnumerable<Kennel>> GetAllAsync()
        {
            return await _context.Kennels.ToListAsync();
        }

        public async Task<Kennel?> GetByIdAsync(int id)
        {
            return await _context.Kennels.FindAsync(id);
        }

        public async Task<Kennel?> RemoveAsync(int id)
        {
            var kennelModel = await _context.Kennels.FindAsync(id);
            if (kennelModel == null)
            {
                return null;
            }
            _context.Kennels.Remove(kennelModel);
            await _context.SaveChangesAsync();
            return kennelModel;
        }

        public async Task<Kennel?> UpdateAsync(int id, Kennel kennelModel)
        {
            var existingKennel = await GetByIdAsync(id);
            existingKennel.Description = kennelModel.Description;
            existingKennel.DailyCost = kennelModel.DailyCost;
            await _context.SaveChangesAsync();
            return existingKennel;
        }
    }
}
