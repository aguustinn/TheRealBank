using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheRealBank.Contexts;
using TheRealBank.Entities;

namespace TheRealBank.Repositories.Users
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly MainContext db;

        public CustomerRepository(MainContext db) => this.db = db;

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
            => await db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken ct = default)
            => await db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CPF == cpf, ct);

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct = default)
            => await db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email, ct);

        public async Task<List<Customer>> GetAllAsync(CancellationToken ct = default)
            => await db.Customers.AsNoTracking().OrderBy(c => c.Nome).ToListAsync(ct);

        public async Task<Customer> AddAsync(Customer customer, CancellationToken ct = default)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            db.Customers.Add(customer);
            await db.SaveChangesAsync(ct);
            return customer;
        }

        public async Task UpdateAsync(Customer customer, CancellationToken ct = default)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));
            db.Customers.Update(customer);
            await db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await db.Customers.FindAsync(new object?[] { id }, ct);
            if (entity is null) return;
            db.Customers.Remove(entity);
            await db.SaveChangesAsync(ct);
        }

        public async Task DeleteByCpfAsync(string cpf, CancellationToken ct = default)
        {
            var entity = await db.Customers.FirstOrDefaultAsync(c => c.CPF == cpf, ct);
            if (entity is null) return;
            db.Customers.Remove(entity);
            await db.SaveChangesAsync(ct);
        }

        public async Task<Customer?> GetByPixKeyAsync(string keyPix, CancellationToken ct = default)
            => await db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.KeyPix == keyPix, ct);

        public async Task<TransferResult> TransferAsync(string senderEmail, string pixKey, decimal amount, string? description = null, CancellationToken ct = default)
        {
            if (amount <= 0m)
                return new TransferResult(TransferStatus.InvalidAmount);

            // Inicia transação para garantir atomicidade
            await using var trx = await db.Database.BeginTransactionAsync(ct);

            var sender = await db.Customers.FirstOrDefaultAsync(c => c.Email == senderEmail, ct);
            if (sender is null)
                return new TransferResult(TransferStatus.SenderNotFound);

            // Localiza destinatário: prioridade chave PIX; fallback email/CPF
            var receiver = await db.Customers.FirstOrDefaultAsync(
                c => c.KeyPix == pixKey || c.Email == pixKey || c.CPF == pixKey,
                ct);

            if (receiver is null)
                return new TransferResult(TransferStatus.ReceiverNotFound);

            if (sender.Saldo < amount)
                return new TransferResult(TransferStatus.InsufficientFunds, sender.Saldo);

            // Debita / Credita
            sender.Saldo -= amount;
            receiver.Saldo += amount;

            db.Customers.Update(sender);
            db.Customers.Update(receiver);

            await db.SaveChangesAsync(ct);
            await trx.CommitAsync(ct);

            // (Opcional: registrar histórico / log usando description)
            return new TransferResult(TransferStatus.Success, sender.Saldo);
        }
    }
}