using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Record record)
        {
            await _context.Records.AddAsync(record);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            return await _context.Records
                .Include(a => a.Pet)
                .ToListAsync();
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAllAppointmentDetailAsync()
        {
            return await _context.AppointmentDetails.ToListAsync();
        }

        public async Task<Record?> GetByIdAsync(int id)
        {

            return await _context.Records
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(x => x.RecordId == id);

        }

        public async Task<bool> RemoveAsync(Record record)
        {
             _context.Records.Remove(record);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Record record)
        {
            _context.Entry(record).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
