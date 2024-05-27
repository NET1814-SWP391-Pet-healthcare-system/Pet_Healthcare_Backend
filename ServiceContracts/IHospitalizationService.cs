using Entities;
using ServiceContracts.DTO.HospitalizationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IHospitalizationService
    {
        bool AddHospitalization(Hospitalization? request);
        Hospitalization? GetHospitalizationById(int id);
        IEnumerable<Hospitalization> GetHospitalizations();
        bool UpdateHospitalization(int id, Hospitalization? request);
        bool RemoveHospitalization(int id);
    }
}