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

        public async Task<VaccineDTO?> AddVaccine(VaccineAddRequest vaccineAddRequest)
        {
            var vaccine = vaccineAddRequest.ToVaccine();
            var isAdded = await _vaccineRepository.AddAsync(vaccine);
            if(isAdded)
            {
                return vaccine.ToVaccineDto();
            }
            return null;
        }

        public async Task<IEnumerable<VaccineDTO>> GetAllVaccines()
        {
            var vaccineList = await _vaccineRepository.GetAllAsync();
            List<VaccineDTO> result = new List<VaccineDTO>();
            foreach(var vaccine in vaccineList)
            {
                result.Add(vaccine.ToVaccineDto());
            }
            return result;
        }

        public async Task<VaccineDTO?> GetVaccineById(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if(vaccine == null)
            {
                return null;
            }
            return vaccine.ToVaccineDto();
        }

        public async Task<bool> RemoveVaccine(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            return await _vaccineRepository.Remove(vaccine);
        }

        public async Task<VaccineDTO?> UpdateVaccine(int id, VaccineUpdateRequest vaccineUpdateRequest)
        {
            var existingVaccine = await _vaccineRepository.GetByIdAsync(id);
            if(existingVaccine == null)
            {
                return null;
            }
            var vaccine = vaccineUpdateRequest.ToVaccine();
            var updatedVaccine = await _vaccineRepository.Update(id, vaccine);
            return updatedVaccine.ToVaccineDto();
        }
    }
}
