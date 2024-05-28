using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Entities.Enum;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    [Table("User")]
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }   
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActive { get; set; }
    }
}