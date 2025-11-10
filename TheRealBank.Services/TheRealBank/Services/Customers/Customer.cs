using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)] // Ajuda na validação e na renderização do input
        public DateTime DataNascimento { get; set; }
    }
}
