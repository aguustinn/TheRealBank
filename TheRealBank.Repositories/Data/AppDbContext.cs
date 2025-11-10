using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SeuProjeto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public decimal Saldo { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
