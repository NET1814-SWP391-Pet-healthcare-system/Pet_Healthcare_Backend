using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>>  GetAll()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetById(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);
        }

        public async Task<bool> Remove(int id)
        {
            var service = await GetById(id);
            if (service == null)
            {
                return false;
            }
            _context.Services.Remove(service);
            return await SaveChangesAsync();
        }

        public async Task<bool> Update(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<Service?> GetByName(string name)
        {
            return await _context.Services.FirstOrDefaultAsync(a => EF.Functions.Like(a.Name, name)); 
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
