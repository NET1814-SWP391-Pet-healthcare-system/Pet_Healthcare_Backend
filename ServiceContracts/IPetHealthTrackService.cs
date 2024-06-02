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
        Task<PetHealthTrack>? AddPetHealthTrackAsync(PetHealthTrack request);
        Task<PetHealthTrack>? GetPetHealthTrackByIdAsync(int id);
        Task<IEnumerable<PetHealthTrack>> GetPetHealthTracksAsync();
        Task<PetHealthTrack>? UpdatePetHealthTrackAsync(PetHealthTrack request);
        Task<PetHealthTrack>? RemovePetHealthTrackAsync(int id);
    }
}
