using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        Task<Customer?> GetCustomerWithPetsAsync(string customerId);
        Task<IEnumerable<Vet>?> GetAvailableVetsAsync(DateOnly date, int slotId);
    }
}
