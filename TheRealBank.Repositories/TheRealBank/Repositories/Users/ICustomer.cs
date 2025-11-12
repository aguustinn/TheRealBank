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
        Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<List<Customer>> GetAllAsync(CancellationToken ct = default);
        Task<Customer> AddAsync(Customer customer, CancellationToken ct = default);
        Task UpdateAsync(Customer customer, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task DeleteByCpfAsync(string cpf, CancellationToken ct = default);
        Task<Customer?> GetByPixKeyAsync(string keyPix, CancellationToken ct = default);
        Task<TransferResult> TransferAsync(string senderEmail, string pixKey, decimal amount, string? description = null, CancellationToken ct = default);
    }

    public enum TransferStatus
    {
        Success,
        SenderNotFound,
        ReceiverNotFound,
        InvalidAmount,
        InsufficientFunds
    }

    public sealed record TransferResult(TransferStatus Status, decimal? NewSenderBalance = null);
}
