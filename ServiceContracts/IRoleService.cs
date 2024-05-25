using Entities;
using ServiceContracts.DTO.RoleDTO;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IRoleService
    {
        bool AddRole(RoleAddRequest request);
        Role? GetRoleById(int id);
        IEnumerable<Role> GetRoles();
        bool UpdateRole(RoleUpdateRequest request);
        bool RemoveRole(int id);
    }
}
