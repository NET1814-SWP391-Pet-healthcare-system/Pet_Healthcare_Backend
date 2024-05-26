using Entities;
using Entities.Constants;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Add(User? user)
        {
            if(user == null)
            {
                return false;
            }
            _context.Users.Add(user);
            SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Role).ToList();
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            return _context.Customers.Include(u => u.Role).ToList();
        }

        public IEnumerable<Vet> GetAllVet()
        {
            return _context.Vets.Include(u => u.Role).ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);  
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(x => x.Username == username);
        }

        public bool Remove(string username)
        {
            var user = GetByUsername(username);
            _context.Users.Remove(user);
            SaveChanges();
            return true;
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        public bool Update(User user)
        {
            var existingUser = GetById(user.UserId);
            if(existingUser == null)
            {
                return false;
            }
            existingUser.RoleId = user.RoleId;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Gender = user.Gender;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;
            existingUser.Country = user.Country;
            existingUser.ImageURL = user.ImageURL;
            existingUser.IsActive = user.IsActive;
            SaveChanges();
            return true;
        }
    }
}
