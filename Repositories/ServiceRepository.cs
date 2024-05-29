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
        public async Task<Service> Add(Service service)
        {
            if(service == null)
            {
                return null;
            }
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<IEnumerable<Service>>  GetAll()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetById(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);
        }

        public async Task<Service?> Remove(int id)
        {
            var service = await GetById(id);
            if (service == null)
            {
                return null;
            }
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> Update(int id,Service service)
        {
            var existService = await GetById(id);
            if(existService == null)
            {
                return null;
            }
            existService.Name = service.Name;
            existService.Description = service.Description;
            existService.Cost = service.Cost;
            await _context.SaveChangesAsync();
            return existService;
        }
    }
}
