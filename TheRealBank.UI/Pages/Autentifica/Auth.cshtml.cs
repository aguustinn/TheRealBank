using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TheRealBank.Repositories.Users;

namespace TheRealBank.UI.Pages.Autentifica
{
    public class AuthModel : PageModel
    {
        private readonly ICustomerRepository _customers;

        public AuthModel(ICustomerRepository customers)
        {
            _customers = customers;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? SuccessMessage { get; private set; }

        // Classe para definir os campos do formulário
        public class InputModel
        {
            [Required(ErrorMessage = "O e-mail é obrigatório.")]
            [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // ==================================================================
            // ÁREA DE VERIFICAÇÃO DE LOGIN (COM BANCO DE DADOS)
            // ==================================================================
            // Busca por e-mail
            var user = await _customers.GetByEmailAsync(Input.Email);
            if (user is null || user.Senha != Input.Password)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return Page();
            }
            // ==================================================================

            // Sucesso: redireciona para a página solicitada
            return RedirectToPage("/Experiencia/Layout"); // equivalente a href="/Experiencia/Layout"
            // Se preferir, use: return Redirect("/Experiencia/Layout");
        }
    }
}