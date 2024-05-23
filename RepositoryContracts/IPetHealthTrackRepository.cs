using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IPetHealthTrackRepository
    {
        IEnumerable<PetHealthTrack> GetAll();
        PetHealthTrack? GetById(int id);
        bool Add(PetHealthTrack petHealthTrack);
        bool Update(PetHealthTrack petHealthTrack);
        bool Remove(int id);
        bool SaveChanges();
    }
}
