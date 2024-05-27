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
        public bool Add(Role? role)
        {
            if(role == null) return false;

            _context.Roles.Add(role);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role? GetById(int id)
        {
            if(id == 0) return null;

            return _context.Roles.Find(id);
        }
        public bool Remove(int id)
        {
            var roleToRemove = GetById(id);
            if (roleToRemove == null)
                return false;

            _context.Roles.Remove(roleToRemove);
            _context.SaveChanges();
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

        public bool Update(Role? role)
        {
            if (role == null)
            {
                return false;
            }
            _context.Roles.Update(role);
            return true;
        }
    }
}
