using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Kennel")]
    public class Kennel
    {
        [Key]
        public int KennelId { get; set;}
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public double DailyCost { get; set; }
        [JsonIgnore]
        public ICollection<Hospitalization>? Hospitalizations { get; set; }
    }
}