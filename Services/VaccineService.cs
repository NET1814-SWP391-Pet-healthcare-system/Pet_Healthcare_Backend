using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.VaccineDTO;
using ServiceContracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepository;

        public VaccineService(IVaccineRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<Vaccine?> AddVaccine(VaccineAddRequest vaccineAddRequest)
        {
            var vaccine = vaccineAddRequest.ToVaccine();
            var isAdded = await _vaccineRepository.AddAsync(vaccine);
            if (isAdded)
            {
                return vaccine;
            }
            return null;
        }

        public async Task<IEnumerable<Vaccine>> GetAllVaccines()
        {
            return await _vaccineRepository.GetAllAsync();
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            return await _vaccineRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveVaccine(int id)
        {
            var vaccine = await GetVaccineById(id);
            return await _vaccineRepository.Remove(vaccine);
        }

        public async Task<Vaccine?> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if(vaccine == null)
            {
                return null;
            }
            var request = vaccineUpdateRequest.ToVaccine();
            return await _vaccineRepository.Update(id, request);
            
        }
    }
}
