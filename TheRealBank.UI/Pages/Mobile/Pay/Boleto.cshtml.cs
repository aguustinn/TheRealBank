using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TheRealBank.UI.Pages.Mobile.Pay
{
    public class BoletoModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Código de Barras")]
        [Required(ErrorMessage = "Você precisa digitar ou colar o código de barras.")]
        public string CodigoDeBarras { get; set; }

        public decimal SaldoDisponivel { get; private set; }

        public void OnGet()
        {
            // Puxa o saldo (estou usando o valor do seu print)
            SaldoDisponivel = 1110473.88m;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Se o formulário for inválido (código vazio),
                // recarrega o saldo e mostra a página novamente com o erro.
                OnGet();
                return Page();
            }

            // SE O CÓDIGO FOR VÁLIDO:
            // 1. Valide o 'CodigoDeBarras' (verifique se tem 44 ou 48 dígitos, etc.)
            // 2. Decodifique o valor e a data de vencimento
            // 3. Redirecione para uma página de "Confirmação de Pagamento"

            // Por enquanto, vamos apenas redirecionar
            return RedirectToPage("/Experiencia/Layout");
        }
    }
}