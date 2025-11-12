using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheRealBank.UI.Pages.Mobile
{
    public class ReceberModel : PageModel
    {
        public string UserNome { get; set; }
        public string UserChavePix { get; set; }
        public string UserChavePixMascarada { get; set; }

        public void OnGet()
        {
            // Puxando o nome do seu print
            UserNome = "Gabriel";

            // Simule a chave PIX principal do usuário
            UserChavePix = "123.456.789-00";
            UserChavePixMascarada = "123.***.***-00";
        }
    }
}