using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TheRealBank.Contexts.Base;

namespace TheRealBank.Entities
{
    [UniqueIndex(nameof(CPF))]
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Nome { get; set; } = default!;

        [Required, MaxLength(14)]
        public string CPF { get; set; } = default!;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = default!;

        [Precision(18, 2)]
        public decimal Saldo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
