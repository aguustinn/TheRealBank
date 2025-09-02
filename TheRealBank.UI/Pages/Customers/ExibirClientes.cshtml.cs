using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheRealBank.Services.Customers;

namespace TheRealBank.UI.Pages.Customers
{
    public class ExibirClientesModel : PageModel
    {
        private readonly ICustomerService _customerService;

        // Aqui trocamos para lista em vez de 1 cliente
        public List<Services.Customers.Customer> Clientes { get; set; } = new();

        public ExibirClientesModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Clientes = await _customerService.GetCustomersAsync();
            return Page();
        }
    }

}
