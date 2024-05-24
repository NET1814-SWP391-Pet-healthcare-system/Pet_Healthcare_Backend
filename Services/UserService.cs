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
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
