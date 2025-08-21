using Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheRealBank.UI.Pages.Customer
{
    public class ExibirClientesModel : PageModel
    {
        private readonly ICustomerService _customerService;

        // Aqui trocamos para lista em vez de 1 cliente
        public List<Customers.Customer> Clientes { get; set; } = new();

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
