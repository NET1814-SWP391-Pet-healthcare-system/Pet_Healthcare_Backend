using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public bool Add(Payment payment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Payment? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
