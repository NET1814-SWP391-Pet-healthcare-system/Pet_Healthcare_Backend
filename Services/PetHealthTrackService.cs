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


        public async Task<PetHealthTrackDTO?> AddPetHealthTrackAsync(PetHealthTrackAddRequest request)
        {
            var petHealthTrack = await _petHealthTrackRepository.AddAsync(request.ToPetHealthTrack());

            return petHealthTrack?.ToPetHealthTrackDTO() as PetHealthTrackDTO; 
        }

     

        public async Task<PetHealthTrackDTO?> GetPetHealthTrackByIdAsync(int id)
        {
            var petHealthTrack = await _petHealthTrackRepository.GetByIdAsync(id);
            return petHealthTrack?.ToPetHealthTrackDTO() as PetHealthTrackDTO;
        }


        public async Task<IEnumerable<PetHealthTrack>> GetPetHealthTracksAsync()
        {

            return await _petHealthTrackRepository.GetAllAsync();
        }

        public async Task<PetHealthTrackDTO>? RemovePetHealthTrackAsync(int id)
        {
            
            var result = await _petHealthTrackRepository.RemoveAsync(id);
            return result.ToPetHealthTrackDTO();
        }


        public async Task<PetHealthTrackDTO>? UpdatePetHealthTrackAsync(PetHealthTrackUpdateRequest request)
        {
            var existingPHT = await _petHealthTrackRepository.GetByIdAsync(request.PetHealthTrackId);
            if (existingPHT == null) return null;
            if (request == null) return null;
            var phtModel = request;
            existingPHT.HospitalizationId = phtModel.HospitalizationId;
            existingPHT.Date = phtModel.DateOnly;
            existingPHT.Description = phtModel.Description;
            existingPHT.Status = phtModel.Status;
            _petHealthTrackRepository.UpdateAsync(existingPHT);

           
            return existingPHT.ToPetHealthTrackDTO();
        }
    }
}
