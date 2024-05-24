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
        User? GetUserById(int id);
        IEnumerable<User> GetUsers();
        bool UpdateUser(UserUpdateRequest request);
        bool RemoveUser(int id);
    }
}
