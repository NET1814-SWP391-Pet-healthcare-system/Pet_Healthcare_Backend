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
        public bool Add(Service service)
        {
            if(service == null)
            {
                return false;
            }
            _context.Services.Add(service);
            SaveChanges();
            return true;
        }

        public IEnumerable<Service> GetAll()
        {
            return _context.Services.ToList();  
        }

        public Service? GetById(int id)
        {
            return _context.Services.Find(id);
        }

        public bool Remove(int id)
        {
            if(GetById(id)==null)
            {
                return false;
            }
            _context.Services.Remove(GetById(id));
            SaveChanges();
            return true;
        }

        public bool SaveChanges()
        {
            if (_context.SaveChanges() == 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Service service)
        {
            if(service == null)
            {
                return false;
            }
            _context.Services.Update(service);
            SaveChanges();
            return true;
        }
    }
}
