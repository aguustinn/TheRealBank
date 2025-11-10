using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheRealBank.Entities;

namespace TheRealBank.Repositories.Users
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Customer?> GetByCpfAsync(string cpf, CancellationToken ct = default);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default); // NOVO
        Task<List<Customer>> GetAllAsync(CancellationToken ct = default);
        Task<Customer> AddAsync(Customer customer, CancellationToken ct = default);
        Task UpdateAsync(Customer customer, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task DeleteByCpfAsync(string cpf, CancellationToken ct = default);
    }
}
