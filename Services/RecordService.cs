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

        public bool AddRecord(Record? request)
        {
            return _recordRepository.Add(request);
        }

        public IEnumerable<AppointmentDetail> GetAppointmentDetails()
        {
            return _recordRepository.GetAllAppointmentDetail();
        }

        public Record? GetRecordById(int id)
        {
            return _recordRepository.GetById(id);
        }

        public IEnumerable<Record> GetRecords()
        {
            return _recordRepository.GetAll();
        }

        public bool RemoveRecord(int id)
        {
            return _recordRepository.Remove(id);
        }

        public bool UpdateRecord(int id ,Record? request)
        {
            var existingRecord = _recordRepository.GetById(id);
            if (existingRecord == null) { return false; }
            if (request == null) { return false; }
            return _recordRepository.Update(existingRecord);
        }
    }
}
