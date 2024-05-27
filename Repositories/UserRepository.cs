using Azure.Core;
using Entities;
using Entities.Constants;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;


        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return SaveChanges();
        }

        public bool AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return SaveChanges();
        }
        public bool AddVet(Vet vet)
        {
            _context.Vets.Add(vet);
            return SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.Role)
                .ToList();
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            return _context.Customers
                .Include(c => c.Role)
                .Include(c => c.Pets)
                .ToList();
        }

        public IEnumerable<Vet> GetAllVet()
        {
            return _context.Vets
                .Include(v => v.Role)
                .ToList();
        }

        public Object? GetUserById(int id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
            switch(user.RoleId)
            {
                case 2:
                    Customer customer = _context.Customers
                        .Include(c => c.Role)
                        .Include(c => c.Pets)
                        .FirstOrDefault(c => c.UserId == id);
                    return customer;

                case 3:
                    Vet vet = _context.Vets
                        .Include(v => v.Role)
                        .FirstOrDefault(v => v.UserId == id);
                    return vet;
            }
            //other cases such as Admin, Employee
            return user;
        }

        public bool RemoveUser(Object? user)
        {
            _context.Users.Remove((User)user);
            SaveChanges();
            return true;
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public Object UpdateUser(int id, Object obj)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
            Object result = null;
            switch(user.RoleId)
            {
                case 2:
                    Customer? existingCustomer = _context.Customers.FirstOrDefault(c => c.UserId == id);
                    if(existingCustomer == null)
                    {
                        return false;
                    }
                    Customer customerRequest = obj as Customer;
                    existingCustomer.FirstName = customerRequest.FirstName;
                    existingCustomer.LastName = customerRequest.LastName;
                    existingCustomer.Gender = customerRequest.Gender;
                    existingCustomer.Email = customerRequest.Email;
                    existingCustomer.Username = customerRequest.Username;
                    existingCustomer.Password = customerRequest.Password;
                    existingCustomer.Address = customerRequest.Address;
                    existingCustomer.Country = customerRequest.Country;
                    existingCustomer.ImageURL = customerRequest.ImageURL;
                    existingCustomer.IsActive = customerRequest.IsActive;
                    result = existingCustomer;
                    break;

                case 3:
                    Vet? existingVet = _context.Vets.FirstOrDefault(v => v.UserId == id);
                    if(existingVet == null)
                    {
                        return false;
                    }
                    Vet vetRequest = obj as Vet;
                    existingVet.FirstName = vetRequest.FirstName;
                    existingVet.LastName = vetRequest.LastName;
                    existingVet.Gender = vetRequest.Gender;
                    existingVet.Email = vetRequest.Email;
                    existingVet.Username = vetRequest.Username;
                    existingVet.Password = vetRequest.Password;
                    existingVet.Address = vetRequest.Address;
                    existingVet.Country = vetRequest.Country;
                    existingVet.ImageURL = vetRequest.ImageURL;
                    existingVet.IsActive = vetRequest.IsActive;
                    existingVet.Rating = vetRequest.Rating;
                    existingVet.YearsOfExperience = vetRequest.YearsOfExperience;
                    result = existingVet;
                    break;

                //user, employee, admin
                default:
                    User? existingUser = _context.Users.FirstOrDefault(u => u.UserId == id);
                    if(existingUser == null)
                    {
                        return false;
                    }
                    User userRequest = obj as User;
                    existingUser.FirstName = userRequest.FirstName;
                    existingUser.LastName = userRequest.LastName;
                    existingUser.Gender = userRequest.Gender;
                    existingUser.Email = userRequest.Email;
                    existingUser.Username = userRequest.Username;
                    existingUser.Password = userRequest.Password;
                    existingUser.Address = userRequest.Address;
                    existingUser.Country = userRequest.Country;
                    existingUser.ImageURL = userRequest.ImageURL;
                    existingUser.IsActive = userRequest.IsActive;
                    result = existingUser;
                    break;
            }
            SaveChanges();
            return result;
            
        }
    }
}
