using Entities;
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
        public bool AddPetHealthTrack(PetHealthTrackAddRequest request)
        {
            throw new NotImplementedException();
        }

        public PetHealthTrack? GetPetHealthTrackById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PetHealthTrack> GetPetHealthTracks()
        {
            throw new NotImplementedException();
        }

        public bool RemovePetHealthTrack(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePetHealthTrack(PetHealthTrackUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
