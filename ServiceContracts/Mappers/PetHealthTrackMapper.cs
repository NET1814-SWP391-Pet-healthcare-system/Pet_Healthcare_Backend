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
                Date = request.Date,
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId,
                PetName = request.Hospitalization.Pet.Name,
                PetImage = request.Hospitalization.Pet.ImageURL
            };
        }
        public static PetHealthTrack ToPetHealthTrackFromAdd(this PetHealthTrackAddRequest request)
        {
            return new PetHealthTrack
            {
                Date = DateOnly.Parse(request.Date),
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId
            };
        }
        public static PetHealthTrack ToPetHealthTrackFromUpdate(this PetHealthTrackUpdateRequest request)
        {
            return new PetHealthTrack
            {
                Date = DateOnly.Parse(request.Date),
                Description = request.Description,
                Status = request.Status,
                HospitalizationId = request.HospitalizationId,
            };
        }
    }
}
