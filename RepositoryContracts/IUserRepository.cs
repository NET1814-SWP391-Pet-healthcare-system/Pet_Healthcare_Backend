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
        IEnumerable<Customer> GetAllCustomer();
        IEnumerable<Vet> GetAllVet();
        Object? GetUserById(int id);
        bool AddUser(User user);
        bool AddCustomer(Customer customer);
        bool AddVet(Vet vet);
        Object? UpdateUser(int id, Object user);
        bool RemoveUser(Object? user);
        bool SaveChanges();
    }
}
