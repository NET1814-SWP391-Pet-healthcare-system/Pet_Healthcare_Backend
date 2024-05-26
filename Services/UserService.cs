using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public bool AddUser(UserAddRequest? request)
        {
            if(request == null)
            {
                return false;
            }
            var user = request.ToUser();
            //user.UserId = new Guid();
            _userRepository.Add(user);
            return true;
        }

        public User? GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }

        public User? GetUserByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return _userRepository.GetAllCustomer();
        }

        public IEnumerable<Vet> GetVets()
        {
            return _userRepository.GetAllVet();
        }

        public bool RemoveUser(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if(user == null)
            {
                return false;
            }
            return _userRepository.Remove(username);

        }

        public bool UpdateUser(UserUpdateRequest request)
        {
            var user = _userRepository.GetById(request.UserId);
            if(user == null)
            {
                return false;
            }
            
            return _userRepository.Update(request.ToUser());
            
        }
    }
}
