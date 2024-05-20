using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IInvoiceRepository
    {
        IEnumerable<Invoice> GetAll();
        Invoice GetById(int id);
        bool Add(Invoice invoice);
        bool Update(Invoice invoice);
        bool Remove(int id);
        bool SaveChanges();
    }
}
