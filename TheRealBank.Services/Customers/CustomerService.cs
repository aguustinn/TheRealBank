// Customers/CustomerService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers
{
    public class CustomerService : ICustomerService
    {
        // A lista agora é 'private static' para simular um "banco de dados" em memória.
        // A inicialização foi corrigida para adicionar clientes com dados de exemplo.
        private static readonly List<Customer> _clientes = new List<Customer>
        {
            new Customer { Nome = "Ana Silva", CPF = "111.222.333-44", Email = "ana.silva@email.com", Saldo = 1500.75m },
            new Customer { Nome = "Bruno Costa", CPF = "222.333.444-55", Email = "bruno.costa@email.com", Saldo = 320.50m },
            new Customer { Nome = "Carlos Dias", CPF = "333.444.555-66", Email = "carlos.dias@email.com", Saldo = 9870.00m }
        };

        /// <summary>
        /// Obtém a lista completa de clientes de forma assíncrona.
        /// </summary>
        /// <returns>Uma Task contendo a lista de clientes.</returns>
        public async Task<List<Customer>> GetCustomersAsync()
        {
            // Simula uma chamada de rede ou acesso a banco de dados.
            await Task.Delay(50); // Pequeno atraso para simular assincronia
            return _clientes;
        }

        /// <summary>
        /// Adiciona um novo cliente à lista de forma assíncrona.
        /// </summary>
        /// <param name="newCustomer">O objeto Customer a ser adicionado.</param>
        public async Task AddCustomerAsync(Customer newCustomer)
        {
            if (newCustomer == null)
            {
                // Lança uma exceção se o cliente for nulo
                throw new ArgumentNullException(nameof(newCustomer));
            }

            // Simula uma chamada de rede ou escrita no banco de dados.
            await Task.Delay(50);
            _clientes.Add(newCustomer);
        }


        public async Task<Customer> GetCustomerByCpfAsync(string cpf)
        {
            // Simula uma busca no banco de dados
            await Task.Delay(50);

            // Usa LINQ para encontrar o primeiro cliente com o CPF correspondente
            var cliente = _clientes.FirstOrDefault(c => c.CPF == cpf);

            return cliente; // Retorna o cliente encontrado ou null se não encontrar
        }
    }
}