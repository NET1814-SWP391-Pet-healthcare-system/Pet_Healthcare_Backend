using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int roleId { get; set; }
        public string name { get; set; }
        public ICollection<User> users { get; set; }
    }
}
