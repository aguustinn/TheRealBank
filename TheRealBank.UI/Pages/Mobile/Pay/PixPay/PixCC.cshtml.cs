using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TheRealBank.UI.Pages.Mobile.Pay
{
    public class CopiaColaModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Você precisa colar um código PIX")]
        [Display(Name = "Código PIX Copia e Cola")]
        public string PixCode { get; set; }

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
                SaldoDisponivel = 1110473.88m;
                return Page();
            }

            // SE O CÓDIGO FOR VÁLIDO:
            // 1. Decodifique o 'PixCode'
            // 2. Busque os dados do destinatário
            // 3. Redirecione para uma página de "Confirmação"
            //    passando os dados (Ex: return RedirectToPage("ConfirmarPix", new { code = PixCode });)

            // Por enquanto, vamos apenas redirecionar de volta para a Home
            return RedirectToPage("/Experiencia/Layout");
        }
    }
}