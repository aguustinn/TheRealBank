// Customers/CustomerService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRealBank.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private static readonly List<Customer> _clientes = new List<Customer>
        {
            new Customer { Nome = "Ana Silva", CPF = "111.222.333-44", Email = "ana.silva@email.com", Saldo = 1500.75m },
            new Customer { Nome = "Bruno Costa", CPF = "222.333.444-55", Email = "bruno.costa@email.com", Saldo = 320.50m },
            new Customer { Nome = "Carlos Dias", CPF = "333.444.555-66", Email = "carlos.dias@email.com", Saldo = 9870.00m }
        };

        public async Task<List<Customer>> GetCustomersAsync()
        {
            await Task.Delay(50);
            return _clientes;
        }

        public async Task AddCustomerAsync(Customer newCustomer)
        {
            if (newCustomer == null)
            {
                throw new ArgumentNullException(nameof(newCustomer));
            }
            await Task.Delay(50);
            _clientes.Add(newCustomer);
        }

        public async Task<Customer> GetCustomerByCpfAsync(string cpf)
        {
            await Task.Delay(50);
            var cliente = _clientes.FirstOrDefault(c => c.CPF == cpf);
            return cliente;
        }

        // CORREÇÃO 1: Nome do método corrigido de "UpadateAsync" para "UpdateAsync"
        public async Task UpdateAsync(string cpf, Customer clienteAtualizado)
        {
            await Task.Delay(50); // Simula chamada de rede

            // CORREÇÃO 2: Usando FirstOrDefault para evitar exceções
            var clienteExistente = _clientes.FirstOrDefault(c => c.CPF == cpf);

            // Verifica se o cliente foi realmente encontrado antes de atualizar
            if (clienteExistente != null)
            {
                clienteExistente.Nome = clienteAtualizado.Nome;
                clienteExistente.CPF = clienteAtualizado.CPF;
                clienteExistente.DataNascimento = clienteAtualizado.DataNascimento;
                clienteExistente.Saldo = clienteAtualizado.Saldo;
                // CORREÇÃO 3: Faltava atualizar o e-mail
                clienteExistente.Email = clienteAtualizado.Email;
            }
            // Se o cliente não for encontrado, o método simplesmente termina sem erro.
            // Em um cenário real, você poderia lançar uma exceção customizada aqui.
        }

        public async Task DeleteAsync(string cpf)
        {
            await Task.CompletedTask;
            _clientes.RemoveAll(c => c.CPF == cpf);
        }
    }
}