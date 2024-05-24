using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Role role)
        {
            if(role == null) return false;

            _context.Roles.Add(role);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            if (_context.SaveChanges() == 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
