using System;
using System.ComponentModel.DataAnnotations;
using TheRealBank.Contexts.Base;

namespace TheRealBank.Entities
{
    // Ensure unique CPF at database level
    [UniqueIndex(nameof(CPF))]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = default!;

        [Required]
        public string CPF { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Range(0, double.MaxValue)]
        public decimal Saldo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
