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

        // MELHORIA: O par�metro aqui deve corresponder ao da rota (@page "{CPF}") para clareza
        public async Task<IActionResult> OnPostAsync(string CPF)
        {
            // Valida��o do modelo � importante em p�ginas de edi��o
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // CORRE��O: Chamando o m�todo com o nome correto "UpdateAsync"
            await _service.UpdateAsync(CPF, Cliente);

            // Redireciona para a lista de clientes ap�s a atualiza��o
            return RedirectToPage("/Customers/ExibirClientes");
        }
    }
}