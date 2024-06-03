using Entities;
using ServiceContracts.DTO.PetHealthTrackDTO;

namespace ServiceContracts.Mappers
{
    public static class PetHealthTrackMapper
    {
        public static PetHealthTrackDTO ToPetHealthTrackDTO(this PetHealthTrack request)
        {
            return new PetHealthTrackDTO
            {
                PetHealthTrackId = request.PetHealthTrackId,
                DateOnly = request.Date,
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId,
            };
        }

        public static PetHealthTrackDTO ToPetHealthTrackFromAddDTO(this PetHealthTrackAddRequest request)
        {
            return new PetHealthTrackDTO
            {
                DateOnly = request.DateOnly,
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId,
                
            };
        }
        public static PetHealthTrack ToPetHealthTrackFromAdd(this PetHealthTrackAddRequest request)
        {
            return new PetHealthTrack
            {
                Date = request.DateOnly,
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId
            };
        }
        public static PetHealthTrackDTO ToPetHealthTrackFromUpdate(this PetHealthTrackUpdateRequest request)
        {
            return new PetHealthTrackDTO
            {
                DateOnly = request.DateOnly,
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId,
            };
        }

        public static PetHealthTrackDTO ToPetHealthTrackFromRemove(this PetHealthTrackRemoveRequest request)
        {
            return new PetHealthTrackDTO
            {
                PetHealthTrackId = request.PetHealthTrackId
            };
        }
    }
}
