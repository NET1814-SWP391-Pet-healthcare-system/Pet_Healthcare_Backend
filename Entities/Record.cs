using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Record")]
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public int NumberOfVisits { get; set; }
        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }
}