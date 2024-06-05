using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.SlotDTO
{
    public class SlotDto
    {
        public int SlotId { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeSpan? Duration => EndTime - StartTime;
        public bool Available { get; set; }
    }
}
