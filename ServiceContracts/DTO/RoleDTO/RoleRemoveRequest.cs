using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RoleDTO
{
    public class RoleRemoveRequest
    {
        [Required(ErrorMessage = "Must enter a Role ID that existed")]
        public int RoleId { get; set; }
        public Role toRole()
        {
            return new Role
            {
                RoleId = RoleId
            };
        }
    }
}
