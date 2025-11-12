using System;
using System.ComponentModel.DataAnnotations;

namespace TheRealBank.Services.Customers
{
    public class Customer
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public decimal Saldo { get; set; }
        public string? Senha { get; set; }
        public bool Auth { get; set; }
        public string? KeyPix { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
