using Entities;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IUserService
    {
        bool AddUser(UserAddRequest request);
        Object? GetUserById(int id);
        IEnumerable<User> GetUsers();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Vet> GetVets();
        Object? UpdateUser(int id, UserUpdateRequest request);
        bool RemoveUser(int id);
    }
}
