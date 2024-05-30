using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.RecordDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<Record?> AddRecordAsync(Record? request)
        {

            return await _recordRepository.AddAsync(request);
        }

        public async Task<IEnumerable<AppointmentDetail>?> GetAppointmentDetailsAsync()
        {
            return await _recordRepository.GetAllAppointmentDetailAsync();
        }

        public async Task<Record?> GetRecordByIdAsync(int id)
        {
            return await _recordRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Record>?> GetRecordsAsync()
        {
            return await _recordRepository.GetAllAsync();
        }

        public async Task<Record> RemoveRecordAsync(int id)
        {
            return await  _recordRepository.RemoveAsync(id);
        }

        public async Task<Record?> UpdateRecordAsync(int id ,Record? request)
        {
            if(request == null)
            {
                return null;
            }
            var existingRecord = _recordRepository.GetByIdAsync(id).Result;
            if(existingRecord == null)
            {
                return null;
            }

            existingRecord.RecordId = request.RecordId;
            existingRecord.PetId = request.PetId;
            existingRecord.NumberOfVisits = request.NumberOfVisits;
            existingRecord.AppointmentDetails = request.AppointmentDetails;
            existingRecord.Pet = request.Pet;
            return await _recordRepository.UpdateAsync(existingRecord);
        }
    }
}
