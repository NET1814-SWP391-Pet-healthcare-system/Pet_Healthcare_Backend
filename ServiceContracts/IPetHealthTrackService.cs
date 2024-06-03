using Entities;
using ServiceContracts.DTO.PetHealthTrackDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPetHealthTrackService
    {
        Task<PetHealthTrackDTO>? AddPetHealthTrackAsync(PetHealthTrackAddRequest request);
        Task<PetHealthTrackDTO>? GetPetHealthTrackByIdAsync(int id);
        Task<IEnumerable<PetHealthTrack>> GetPetHealthTracksAsync();
        Task<PetHealthTrackDTO>? UpdatePetHealthTrackAsync(PetHealthTrackUpdateRequest request);
        Task<PetHealthTrackDTO>? RemovePetHealthTrackAsync(int id);
    }
}
