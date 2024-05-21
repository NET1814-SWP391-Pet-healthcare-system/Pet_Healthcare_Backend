using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment? GetById(int id);
        bool Add(Payment payment);
        bool Update(Payment payment);
        bool Remove(int id);
        bool SaveChanges();
    }
}
