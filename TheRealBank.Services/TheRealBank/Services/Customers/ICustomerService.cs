using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRealBank.Services.Customers
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer newCustomer);
        Task<List<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerByCpfAsync(string cpf);
        Task UpdateAsync(string cpf, Customer clienteAtualizado);
        Task DeleteAsync(string cpf);
    }
}
