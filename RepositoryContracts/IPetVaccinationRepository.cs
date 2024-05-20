using PetHealthCareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    internal interface IPetVaccinationRepository
    {
        IEnumerable<PetVaccination> GetAll();
        User GetById(int id);       //2 id which are primary key?
        bool Add(PetVaccination petVaccination);
        bool Update(PetVaccination petVaccination);
        bool Remove(int id);    //2 id which are primary key?
        bool SaveChanges();
    }
}
