using Entities;
using Entities.Constants;
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
        public bool AddUser(UserAddRequest request)
        {
            var role = request.RoleId;
            switch(role)
            {
                //Admin
                case 1:
                    return _userRepository.AddUser(request.ToUser());

                //Customer
                case 2:
                    Customer customer = new Customer
                    {
                        //RoleId = request.RoleId,
                        //FirstName = request.FirstName,
                        //LastName = request.LastName,
                        //Gender = request.Gender,
                        //Email = request.Email,
                        //Username = request.Username,
                        //Password = request.Password,
                        //Address = request.Address,
                        //Country = request.Country,
                        //ImageURL = request.ImageURL,
                        //IsActive = request.IsActice
                    };
                    //_userRepository.AddCustomer(customer);
                    break;

                //Vet
                case 3:
                    //Vet vet = new Vet
                    //{
                    //    RoleId = request.RoleId,
                    //    FirstName = request.FirstName,
                    //    LastName = request.LastName,
                    //    Gender = request.Gender,
                    //    Email = request.Email,
                    //    Username = request.Username,
                    //    Password = request.Password,
                    //    Address = request.Address,
                    //    Country = request.Country,
                    //    ImageURL = request.ImageURL,
                    //    IsActive = request.IsActice,
                    //    Rating = request.Rating,
                    //    YearsOfExperience = request.YearsOfExperience
                    //};
                    //_userRepository.AddVet(vet);
                    break;

                //Employee
                case 4:
                    User employee = request.ToUser();
                    _userRepository.AddUser(employee);
                    break;

            }
            return true;
        }

        public Object? GetUserById(int id)
        {
            var obj = _userRepository.GetUserById(id);
            if(obj is Customer customer)
            {
                customer = obj as Customer;
                return customer;
            }
            else if(obj is Vet vet)
            {
                vet = obj as Vet;
                return vet;
            }
            else
            {
                return obj as User;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            //return _userRepository.GetAllCustomer();
            return null;
        }

        public IEnumerable<Vet> GetVets()
        {
            //return _userRepository.GetAllVet();
            return null;
        }

        public bool RemoveUser(int id)
        {
            //use repo because the method need object type
            var obj = _userRepository.GetUserById(id);
            return _userRepository.RemoveUser(obj);

        }

        public Object? UpdateUser(int id, UserUpdateRequest request)
        {
            Object? result = null;
            var obj = _userRepository.GetUserById(id);
            if(obj is Customer customer)
            {
                result = _userRepository.UpdateUser(id, request.ToCustomer());
            }
            else if(obj is Vet vet)
            {
                result = _userRepository.UpdateUser(id, request.ToVet());
            }
            else
            {
                result = _userRepository.UpdateUser(id, request.ToUser());
            }
            return result;
        }
    }
}
