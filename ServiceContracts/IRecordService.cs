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
        Task<Record?> GetRecordByPetIdAsync(int id);
        Task<IEnumerable<AppointmentDetail>> GetAppointmentDetailsAsync();
        Task<IEnumerable<Record>> GetRecordsAsync();
        Task<Record?> UpdateRecordAsync(Record request);
        Task<bool> RemoveRecordAsync(int id);
    }
}
