using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.RoleDTO
{
    public class RoleAddRequest
    {
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string? Name { get; set; }
        public Role toRole()
        {
            return new Role { 
                RoleId = RoleId,
                Name = Name 
            };
        }
        
    }
}
