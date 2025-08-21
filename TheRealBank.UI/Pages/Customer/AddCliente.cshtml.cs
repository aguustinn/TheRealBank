// Pages/Customer/AddCliente.cshtml.cs

using Customers; // O using pode permanecer
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TheRealBank.UI.Pages.Customer
{
    public class AddClienteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        // CORREÇÃO APLICADA AQUI: Usamos o nome completo "Customers.Customer"
        // para dizer ao compilador exatamente qual tipo estamos usando.
        [BindProperty]
        public Customers.Customer Cliente { get; set; }

        public AddClienteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet()
        {
        }

        // Pages/Customer/AddCliente.cshtml.cs

        // ... (todo o código anterior permanece o mesmo) ...

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customerService.AddCustomerAsync(Cliente);

            // ❌ Linha Antiga:
            // return RedirectToPage("/Customer/Index");

            // ✅ Nova Linha: Redireciona para a página MostrarCliente,
            // passando o CPF do cliente recém-criado como um parâmetro de rota chamado 'id'.
            return RedirectToPage("/Customer/MostrarCliente", new { id = Cliente.CPF });
        }
    }
}