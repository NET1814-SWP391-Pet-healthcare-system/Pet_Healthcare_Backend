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
        bool AddPetHealthTrack(PetHealthTrackAddRequest request);
        PetHealthTrack? GetPetHealthTrackById(int id);
        IEnumerable<PetHealthTrack> GetPetHealthTracks();
        bool UpdatePetHealthTrack(PetHealthTrackUpdateRequest request);
        bool RemovePetHealthTrack(int id);
    }
}
