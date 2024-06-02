using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO.PetHealthTrackDTO;
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


        public async Task<PetHealthTrack>? AddPetHealthTrackAsync(PetHealthTrack request)
        {
           
            return await _petHealthTrackRepository.AddAsync(request);
        }


        public async Task<PetHealthTrack?> GetPetHealthTrackByIdAsync(int id)
        {
            return await _petHealthTrackRepository.GetByIdAsync(id);
        }


        public async Task<IEnumerable<PetHealthTrack>> GetPetHealthTracksAsync()
        {
            return await _petHealthTrackRepository.GetAllAsync();
        }

        public async Task<PetHealthTrack>? RemovePetHealthTrackAsync(int id)
        {
            return await _petHealthTrackRepository.RemoveAsync(id);
        }


        public async Task<PetHealthTrack>? UpdatePetHealthTrackAsync(PetHealthTrack request)
        {
            var existingPHT = await _petHealthTrackRepository.GetByIdAsync(request.PetHealthTrackId);
            if (existingPHT == null) return null;
            if (request == null) return null;
            var phtModel = request;
            existingPHT.HospitalizationId = phtModel.HospitalizationId;
            existingPHT.Hospitalization = phtModel.Hospitalization;
            existingPHT.Date = phtModel.Date;
            existingPHT.Description = phtModel.Description;
            existingPHT.Status = phtModel.Status;
            _petHealthTrackRepository.UpdateAsync(existingPHT);
            return existingPHT;
        }
    }
}
