using Entities;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IRecordService
    {
        bool AddRecord(RecordAddRequest request);
        User? GetUserById(int id);
        User? GetUserByUsername(string username);
        IEnumerable<User> GetUsers();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Vet> GetVets();
        bool UpdateUser(UserUpdateRequest request);
        bool RemoveUser(string username);
    }
}
