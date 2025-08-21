using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer newCustomer);
        Task<List<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerByCpfAsync(string cpf);
    }
}
