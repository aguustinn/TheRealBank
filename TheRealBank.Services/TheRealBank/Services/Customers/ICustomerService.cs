using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheRealBank.Services.Customers
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(Customer newCustomer);
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByCpfAsync(string cpf);
        Task DeleteAsync(string cpf);
        Task UpdateAsync(string cpf, Customer clienteAtualizado);
        Task PromoteToAdminAsync(string cpf);
        Task DemoteFromAdminAsync(string cpf);

        // PIX support
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task SetPixKeyAsync(string email, string keyPix);
    }
}
