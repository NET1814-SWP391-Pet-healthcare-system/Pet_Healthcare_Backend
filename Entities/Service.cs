using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Service")]
    public class Service
    {
        [Key]
        public int serviceId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public ICollection<Appointment> appointments { get; set; }
    }
}