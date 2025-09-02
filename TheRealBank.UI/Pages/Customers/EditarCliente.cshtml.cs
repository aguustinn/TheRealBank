using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheRealBank.Services.Customers;

namespace TheRealBank.UI.Pages.Customers
{
    public class EditarClienteModel : PageModel
    {
        private readonly ICustomerService _service;

        public EditarClienteModel(ICustomerService service)
        {
            _service = service;
        }

        [BindProperty]
        public Customer Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(string cpf)
        {
            Cliente = await _service.GetCustomerByCpfAsync(cpf);
            if (Cliente == null)
            {
                return NotFound();
            }
            return Page();
        }

        // MELHORIA: O parâmetro aqui deve corresponder ao da rota (@page "{CPF}") para clareza
        public async Task<IActionResult> OnPostAsync(string CPF)
        {
            // Validação do modelo é importante em páginas de edição
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // CORREÇÃO: Chamando o método com o nome correto "UpdateAsync"
            await _service.UpdateAsync(CPF, Cliente);

            // Redireciona para a lista de clientes após a atualização
            return RedirectToPage("/Customers/ExibirClientes");
        }
    }
}