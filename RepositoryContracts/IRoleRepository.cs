using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role? GetById(int id);
        bool Add(Role? role);
        bool Update(Role? role);
        bool Remove(int id);
        bool SaveChanges();
    }
}
