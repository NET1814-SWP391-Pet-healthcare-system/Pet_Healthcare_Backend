using PetHealthCareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool Add(User user);
        bool Update(User user);
        bool Remove(int id);
        bool SaveChanges();
    }
}
