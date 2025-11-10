using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRealBank.Repositories.Users;
using Ent = TheRealBank.Entities.Customer;

namespace TheRealBank.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repo;

        public CustomerService(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var list = await repo.GetAllAsync();
            return list.Select(MapToDto).ToList();
        }

        public async Task AddCustomerAsync(Customer newCustomer)
        {
            if (newCustomer == null) throw new ArgumentNullException(nameof(newCustomer));
            var entity = MapToEntity(newCustomer);
            await repo.AddAsync(entity);
        }

        public async Task<Customer> GetCustomerByCpfAsync(string cpf)
        {
            var entity = await repo.GetByCpfAsync(cpf);
            return entity is null ? null! : MapToDto(entity);
        }

        public async Task UpdateAsync(string cpf, Customer clienteAtualizado)
        {
            if (clienteAtualizado == null) throw new ArgumentNullException(nameof(clienteAtualizado));
            var entity = await repo.GetByCpfAsync(cpf);
            if (entity is null) return;

            entity.Nome = clienteAtualizado.Nome ?? entity.Nome;
            entity.CPF = clienteAtualizado.CPF ?? entity.CPF;
            entity.Email = clienteAtualizado.Email ?? entity.Email;
            entity.Saldo = clienteAtualizado.Saldo;
            entity.DataNascimento = clienteAtualizado.DataNascimento;
            entity.Senha = clienteAtualizado.Senha ?? entity.Senha;
            entity.Auth = clienteAtualizado.Auth;

            await repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string cpf)
        {
            await repo.DeleteByCpfAsync(cpf);
        }

        public async Task PromoteToAdminAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return;
            var entity = await repo.GetByCpfAsync(cpf);
            if (entity is null || entity.Auth) return;
            entity.Auth = true;
            await repo.UpdateAsync(entity);
        }

        public async Task DemoteFromAdminAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return;
            var entity = await repo.GetByCpfAsync(cpf);
            if (entity is null || !entity.Auth) return;
            entity.Auth = false;
            await repo.UpdateAsync(entity);
        }

        private static Ent MapToEntity(Customer c) => new Ent
        {
            Nome = c.Nome ?? string.Empty,
            CPF = c.CPF ?? string.Empty,
            Email = c.Email ?? string.Empty,
            Saldo = c.Saldo,
            DataNascimento = c.DataNascimento,
            Senha = c.Senha ?? string.Empty,
            Auth = c.Auth
        };

        private static Customer MapToDto(Ent e) => new Customer
        {
            Nome = e.Nome,
            CPF = e.CPF,
            Email = e.Email,
            Saldo = e.Saldo,
            DataNascimento = e.DataNascimento,
            Senha = e.Senha,
            Auth = e.Auth
        };
    }
}