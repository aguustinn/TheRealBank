using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TheRealBank.UI.Pages.Mobile.Pay
{
    // Modelo para simular um contato
    public class ContatoFrequente
    {
        public string Nome { get; set; }
        public string Info { get; set; } // Ex: "CPF: ***.123.456-**"
        public string Icone { get; set; } // Ex: "fa-solid fa-user"
    }

    public class TransferirModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Display(Name = "Valor")]
        public decimal? Valor { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A chave é obrigatória")]
        [Display(Name = "Chave PIX ou CPF/CNPJ")]
        public string Chave { get; set; }

        [BindProperty]
        [Display(Name = "Descrição (Opcional)")]
        public string Descricao { get; set; }

        public decimal SaldoDisponivel { get; private set; }
        public List<ContatoFrequente> Contatos { get; private set; }

        public void OnGet()
        {
            // Puxa o saldo (estou usando o valor do seu print)
            SaldoDisponivel = 1110473.88m;

            // Simula uma lista de contatos recentes
            Contatos = new List<ContatoFrequente>
            {
                new ContatoFrequente { Nome = "Maria Silva", Info = "PIX: maria@email.com", Icone = "fa-solid fa-user" },
                new ContatoFrequente { Nome = "João Santos", Info = "Ag: 0001 C: 12345-6", Icone = "fa-solid fa-user-tie" },
                new ContatoFrequente { Nome = "Padaria Pão Quente", Info = "CNPJ: **.345.678/0001-**", Icone = "fa-solid fa-store" }
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Se o formulário for inválido, recarrega os dados do OnGet
                OnGet();
                return Page();
            }

            // Se o formulário for válido:
            // 1. Verifique se o SaldoDisponivel é maior que o Valor
            // 2. Valide a Chave PIX/Conta
            // 3. Redirecione para uma página de Confirmação

            // Por enquanto, vamos apenas redirecionar de volta para a Home
            return RedirectToPage("/Experiencia/Layout");
        }
    }
}