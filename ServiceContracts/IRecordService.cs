using Entities;
using ServiceContracts.DTO.RecordDTO;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IRecordService
    {
        bool AddRecord(Record? request);
        Record? GetRecordById(int id);
        IEnumerable<AppointmentDetail> GetAppointmentDetails();
        IEnumerable<Record> GetRecords();
        bool UpdateRecord(int id,Record? request);
        bool RemoveRecord(int id);
    }
}
