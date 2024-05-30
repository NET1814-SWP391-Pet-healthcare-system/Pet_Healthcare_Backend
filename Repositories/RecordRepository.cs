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

        public async Task<Record> AddAsync(Record record)
        {
            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<IEnumerable<AppointmentDetail>?> GetAllAppointmentDetailAsync()
        {
            return await _context.AppointmentDetails.ToListAsync();
        }

        public async Task<Record?> GetByIdAsync(int id)
        {
            
            var record = await _context.Records.FirstOrDefaultAsync(x => x.RecordId == id);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Record?> RemoveAsync(int id)
        {
            var record = await GetByIdAsync(id);
            if(record ==null)
            {
                return null;
            }

             _context.Records.Remove(record);
            await _context.SaveChangesAsync();
            return record;
        }

     

        public async Task<Record?> UpdateAsync(Record record)
        {
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
