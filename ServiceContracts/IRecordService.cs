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
        Task<Record?> AddRecordAsync(Record? request);
        Task<Record?> GetRecordByIdAsync(int id);
        Task<IEnumerable<AppointmentDetail>?> GetAppointmentDetailsAsync();
        Task<IEnumerable<Record>?> GetRecordsAsync();
        Task<Record?> UpdateRecordAsync(int id,Record? request);
        Task<Record?> RemoveRecordAsync(int id);
    }
}
