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
    public class PetHealthTrackService /*: IPetHealthTrackService*/
    {
        //private readonly IPetHealthTrackRepository _petHealthTrackRepository;
        //public PetHealthTrackService(IPetHealthTrackRepository petHealthTrackRepository)
        //{
        //    _petHealthTrackRepository = petHealthTrackRepository;
        //}

        //public bool AddPetHealthTrack(PetHealthTrackAddRequest request)
        //{
        //    if(request == null)
        //    {
        //        return false;
        //    }
        //    var pht = request.toPetHealthTrack();
            
        //    return _petHealthTrackRepository.Add(pht);
        //}

        //public PetHealthTrack? GetPetHealthTrackById(int id)
        //{
        //    return _petHealthTrackRepository.GetById(id);
        //}

        //public IEnumerable<PetHealthTrack> GetPetHealthTracks()
        //{
        //    return _petHealthTrackRepository.GetAll();
        //}

        //public bool RemovePetHealthTrack(int id)
        //{
        //    var pht = _petHealthTrackRepository.GetById(id);
        //    if(pht == null)
        //    {
        //        return false;
        //    }

        //    return _petHealthTrackRepository.Remove(id);
        //}

        //public bool UpdatePetHealthTrack(PetHealthTrackUpdateRequest request)
        //{
        //    var existingPHT = _petHealthTrackRepository.GetById(request.PetHealthTrackId);
        //    if(existingPHT == null) return false;
        //    if (request == null) return false;
        //    var phtModel = request.toPetHealthTrack();
        //    existingPHT.HospitalizationId = phtModel.HospitalizationId;
        //    existingPHT.Hospitalization = phtModel.Hospitalization;
        //    existingPHT.Date = phtModel.Date;
        //    existingPHT.Description = phtModel.Description;
        //    existingPHT.Status = phtModel.Status;
        //    return _petHealthTrackRepository.Update(existingPHT);
        //}
    }
}
