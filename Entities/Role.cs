using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role
    {
        [Key]
        public int roleId { get; set; }
        public string name { get; set; }
        public ICollection<User> users { get; set; }
    }
}
