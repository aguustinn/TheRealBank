using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Web;
using TheRealBank.Services.Customers;

namespace TheRealBank.UI.Pages.Mobile.Pay.PixPay
{
    public class ReceberModel : PageModel
    {
        private readonly ICustomerService _customers;

        public ReceberModel(ICustomerService customers) => _customers = customers;

        // Dados para exibição
        public string UserNome { get; private set; } = "Cliente";
        public string UserChavePix { get; private set; } = "";
        public string UserChavePixMascarada { get; private set; } = "";
        public string QrCodeData { get; private set; } = "";

        // Permite definir um valor pré-fixado no QR (opcional futuro)
        [FromQuery] public decimal? valor { get; set; }

        public async Task<IActionResult> OnGetAsync(string? chave = null)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToPage("/Autentifica/Auth");

            var customer = await _customers.GetCustomerByEmailAsync(email);
            if (customer is null)
                return RedirectToPage("/Autentifica/Auth");

            UserNome = customer.Nome ?? "Cliente";

            // Determina a chave que será usada:
            // 1) query ?chave=... (forçar outra chave)
            // 2) KeyPix salva
            // 3) fallback: email
            // 4) último fallback: CPF
            var baseKey = !string.IsNullOrWhiteSpace(chave)
                ? chave
                : (!string.IsNullOrWhiteSpace(customer.KeyPix)
                    ? customer.KeyPix
                    : (!string.IsNullOrWhiteSpace(customer.Email)
                        ? customer.Email
                        : customer.CPF));

            UserChavePix = baseKey;
            UserChavePixMascarada = MaskKey(baseKey);

            // Monta payload simples (você pode futuramente gerar payload EMV completo)
            // Inclui valor se enviado (?valor=123.45)
            var rawPayload = valor.HasValue
                ? $"PIX|KEY={baseKey}|VAL={valor.Value:0.00}"
                : $"PIX|KEY={baseKey}";

            QrCodeData = HttpUtility.UrlEncode(rawPayload);

            return Page();
        }

        private static string MaskKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "***";
            // CPF (11 dígitos)
            var digits = new string(key.Where(char.IsDigit).ToArray());
            if (digits.Length == 11)
                return $"{digits[..3]}.***.***-{digits[^2..]}";

            // Email
            var at = key.IndexOf('@');
            if (at > 1)
                return $"{key[0]}***{key[at..]}";

            // Genérica / aleatória: mostra início + ***
            return key.Length > 6 ? key[..4] + "***" : "***";
        }
    }
}