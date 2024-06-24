using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetHealthTrackDTO;
using ServiceContracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PetHealthTrackService : IPetHealthTrackService
    {
        private readonly IPetHealthTrackRepository _petHealthTrackRepository;

        public PetHealthTrackService(IPetHealthTrackRepository petHealthTrackRepository)
        {
            _petHealthTrackRepository = petHealthTrackRepository;
        }


        public async Task<PetHealthTrack?> AddPetHealthTrackAsync(PetHealthTrack request)
        {
            if (request == null)
            {
                return null;
            }
             await _petHealthTrackRepository.AddAsync(request);

            return request;
        }

     

        public async Task<PetHealthTrack?> GetPetHealthTrackByIdAsync(int id)
        {
            var petHealthTrack = await _petHealthTrackRepository.GetByIdAsync(id);
            if (petHealthTrack == null)
            {
                return null;
            }
            return petHealthTrack;
        }


        public async Task<IEnumerable<PetHealthTrack>> GetPetHealthTracksAsync()
        {

            return await _petHealthTrackRepository.GetAllAsync();
        }

        public async Task<bool> RemovePetHealthTrackAsync(int id)
        {
            var healtht = await _petHealthTrackRepository.GetByIdAsync(id);
            if (healtht == null)
            {
                return false;
            }
            return await _petHealthTrackRepository.RemoveAsync(healtht);
        }


        public async Task<PetHealthTrack?> UpdatePetHealthTrackAsync(PetHealthTrack request)
        {
            var existingPHT = await _petHealthTrackRepository.GetByIdAsync(request.PetHealthTrackId);
            if (existingPHT == null) return null;
            if (request == null) return null;
            var phtModel = request;
            existingPHT.HospitalizationId = phtModel.HospitalizationId == null ? existingPHT.HospitalizationId : phtModel.HospitalizationId;
            existingPHT.Date = phtModel.Date == null ? existingPHT.Date : phtModel.Date ;
            existingPHT.Description = phtModel.Description == null ? existingPHT.Description : phtModel.Description;
            existingPHT.Status = phtModel.Status ==null ? existingPHT.Status : phtModel.Status;
            await _petHealthTrackRepository.UpdateAsync(existingPHT);
            return existingPHT;
        }
    }
}
