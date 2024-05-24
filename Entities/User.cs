using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;

namespace Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role{ get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }   
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActice { get; set; }
    }
}