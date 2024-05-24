using Entities;
using ServiceContracts.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IHospitalizationService
    {
        bool AddHospitalization(UserAddRequest request);
        User? GetHospitalizationById(int id);
        IEnumerable<User> GetHospitalization();
        bool UpdateHospitalization(UserUpdateRequest request);
        bool RemoveHospitalization(int id);
    }
}