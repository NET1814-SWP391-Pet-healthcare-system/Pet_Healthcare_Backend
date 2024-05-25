using Azure.Core;
using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.RoleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public bool AddRole(RoleAddRequest? request)
        {
            if (request == null)
            {
                return false;
            }
            var newRole = request.toRole();
            _roleRepository.Add(newRole);
            return true;
        }

        public Role? GetRoleById(int id)
        {
            return _roleRepository.GetById(id);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.GetAll();
        }

        public bool RemoveRole(int id)
        {
            if (GetRoleById(id) == null)
            {
                return false;
            }
            var role = GetRoleById(id);
            _roleRepository.Remove(id);
            return true;
        }

        public bool UpdateRole(RoleUpdateRequest? request)
        {
            if (request == null || request == null)
            {
                return false;
            }
            var role = request.toRole();
            _roleRepository.Update(role);
            return true;
        }
    }
}
