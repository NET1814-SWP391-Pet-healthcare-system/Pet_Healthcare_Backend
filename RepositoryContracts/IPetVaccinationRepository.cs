using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPetVaccinationRepository
    {
        IEnumerable<PetVaccination> GetAll();
        PetVaccination? GetById(int id);       //2 id which are primary key?
        bool Add(PetVaccination petVaccination);
        bool Update(PetVaccination petVaccination);
        bool Remove(int id);    //2 id which are primary key?
        bool SaveChanges();
    }
}
