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
            if (request == null)
            {
                return null;
            }
            await _recordRepository.AddAsync(request);
            return request;
        }

        public async Task<IEnumerable<AppointmentDetail>?> GetAppointmentDetailsAsync()
        {
            return await _recordRepository.GetAllAppointmentDetailAsync();
        }

        public async Task<Record?> GetRecordByIdAsync(int id)
        {

            var result = await _recordRepository.GetByIdAsync(id);
            if (result == null)
            {
                return null;
            }   
            return result;  
        }

        public async Task<Record?> GetRecordByPetIdAsync(int id)
        {
            return await _recordRepository.GetByPetIdAsync(id);
        }

        public async Task<IEnumerable<Record>?> GetRecordsAsync()
        {
            return await _recordRepository.GetAllAsync();
        }

        public async Task<bool> RemoveRecordAsync(int id)
        {
            var rec = await _recordRepository.GetByIdAsync(id);
            if (rec == null)
            {
                return false;
            }
            return await  _recordRepository.RemoveAsync(rec);
        }

        public async Task<Record?> UpdateRecordAsync(Record request)
        {
            if(request == null)
            {
                return null;
            }
            var existingRecord = await _recordRepository.GetByIdAsync(request.RecordId);
            if(existingRecord == null)
            {
                return null;
            }
            existingRecord.PetId = request.PetId == null ? existingRecord.PetId : request.PetId;
            existingRecord.NumberOfVisits = request.NumberOfVisits == null ? existingRecord.NumberOfVisits : request.NumberOfVisits;
            existingRecord.Pet = request.Pet;
            await _recordRepository.UpdateAsync(existingRecord);
            return request;
        }
    }
}
