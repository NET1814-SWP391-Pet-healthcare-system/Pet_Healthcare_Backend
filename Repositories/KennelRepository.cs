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

        public async Task<bool> AddAsync(Kennel kennelModel)
        {
            await _context.Kennels.AddAsync(kennelModel);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Kennel>> GetAllAsync()
        {
            return await _context.Kennels.ToListAsync();
        }

        public async Task<Kennel?> GetByIdAsync(int id)
        {
            return await _context.Kennels
             .FirstOrDefaultAsync(p => p.KennelId == id); ;
        }

        public async Task<bool> RemoveAsync(Kennel kennel)
        {
            _context.Kennels.Remove(kennel);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Kennel kennelModel)
        {
            _context.Entry(kennelModel).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
