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

        public async Task<Vaccine?> AddVaccine(Vaccine vaccine)
        {
            if(vaccine == null)
            {
                return null;
            }
            await _vaccineRepository.AddAsync(vaccine);
            return vaccine;
        }

        public async Task<IEnumerable<Vaccine>> GetAllVaccines()
        {
            var vaccineList = await _vaccineRepository.GetAllAsync();
            //return vaccineList.Select(vaccine => vaccine.ToVaccineDto());
            return vaccineList;
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            if(vaccine == null)
            {
                return null;
            }
            return vaccine;
        }

        public async Task<bool> RemoveVaccineById(int id)
        {
            var vaccine = await _vaccineRepository.GetByIdAsync(id);
            return await _vaccineRepository.RemoveAsync(vaccine);
        }

        public async Task<Vaccine?> UpdateVaccine(Vaccine vaccine)
        {
            if(vaccine == null)
            {
                return null;
            }
            var existingVaccine = await _vaccineRepository.GetByIdAsync(vaccine.VaccineId);
            existingVaccine.Name = string.IsNullOrEmpty(vaccine.Name) ? existingVaccine.Name : vaccine.Name;
            existingVaccine.Description = string.IsNullOrEmpty(vaccine.Description) ? existingVaccine.Description : vaccine.Description;
            existingVaccine.IsAnnualVaccine = vaccine.IsAnnualVaccine;
            //var vaccine = vaccineUpdateRequest.ToVaccine();
            await _vaccineRepository.UpdateAsync(existingVaccine);
            return existingVaccine;
        }
    }
}
