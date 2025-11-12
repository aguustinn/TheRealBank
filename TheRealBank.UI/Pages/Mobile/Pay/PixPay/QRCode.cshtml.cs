using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheRealBank.UI.Pages.Mobile.Pay
{
    public class QRCodeModel : PageModel
    {
        public string MinhaChavePix { get; set; }

        public void OnGet()
        {
            // Você pode carregar a chave PIX principal do usuário aqui
            MinhaChavePix = "123.456.789-00";
        }
    }
}