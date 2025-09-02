using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TheRealBank.Services.Customers;

namespace TheRealBank.UI.Pages.Customers
{
    public class AddClienteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        [BindProperty]
        public Customer Cliente { get; set; }

        public AddClienteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customerService.AddCustomerAsync(Cliente);

            // CORREÇÃO:
            // O nome do parâmetro no objeto anônimo (aqui, 'cpf')
            // deve ser EXATAMENTE o mesmo nome do parâmetro na diretiva @page da página de destino.
            return RedirectToPage("/Customers/MostrarCliente", new { cpf = Cliente.CPF });
        }
    }
}