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
    }
}