using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);   
        bool Add(User user);
        bool Update(User user);
        bool Remove(string username);
        bool SaveChanges();
    }
}
