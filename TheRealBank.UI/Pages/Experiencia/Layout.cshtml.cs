using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Threading.Tasks;
using TheRealBank.Repositories.Users;

namespace TheRealBank.UI.Pages.Experiencia
{
    public class LayoutModel : PageModel
    {
        private readonly ICustomerRepository _customers;

        public LayoutModel(ICustomerRepository customers)
        {
            _customers = customers;
        }

        public string FirstName { get; private set; } = "Cliente";
        public decimal Saldo { get; private set; } = 0m;

        public async Task OnGetAsync([FromQuery] string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            var customer = await _customers.GetByEmailAsync(email);
            if (customer is null)
                return;

            FirstName = GetFirstName(customer.Nome);
            Saldo = customer.Saldo;
        }

        private static string GetFirstName(string? nome)
            => string.IsNullOrWhiteSpace(nome)
                ? "Cliente"
                : nome.Split(' ', System.StringSplitOptions.RemoveEmptyEntries)[0];
    }
}
