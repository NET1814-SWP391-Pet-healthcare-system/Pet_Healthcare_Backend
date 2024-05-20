using System;
using RepositoryContracts;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    internal class HospitalizationRepository : IHospitalizationRepository
    {
        bool IHospitalizationRepository.Add(Hospitalization hospitalization)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Hospitalization> IHospitalizationRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Hospitalization IHospitalizationRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        bool IHospitalizationRepository.Remove(int id)
        {
            throw new NotImplementedException();
        }

        bool IHospitalizationRepository.SaveChanges()
        {
            throw new NotImplementedException();
        }

        bool IHospitalizationRepository.Update(Hospitalization hospitalization)
        {
            throw new NotImplementedException();
        }
    }
}
