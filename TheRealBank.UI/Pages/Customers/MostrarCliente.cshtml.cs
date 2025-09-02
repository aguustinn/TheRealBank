
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TheRealBank.Services.Customers;

namespace TheRealBank.UI.Pages.Customers
{
    public class MostrarClienteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        // A propriedade Cliente continua igual para ser usada na página.
        public Customer Cliente { get; set; }

        // <<< CORREÇÃO: Removemos a propriedade [BindProperty] para 'Id'.
        // Não precisamos dela se usarmos o parâmetro no método.

        public MostrarClienteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // <<< CORREÇÃO PRINCIPAL:
        // Adicionamos um parâmetro 'cpf' ao método OnGetAsync.
        // O nome 'cpf' deve ser o mesmo da rota na diretiva @page.
        public async Task<IActionResult> OnGetAsync(string cpf)
        {
            // Se o CPF da URL for nulo ou vazio, a página não é encontrada.
            if (string.IsNullOrEmpty(cpf))
            {
                return NotFound();
            }

            // Usa o parâmetro 'cpf' recebido diretamente da URL para buscar o cliente.
            Cliente = await _customerService.GetCustomerByCpfAsync(cpf);

            // Se nenhum cliente for encontrado com esse CPF, retorna erro 404.
            if (Cliente == null)
            {
                return NotFound();
            }

            // Se encontrou, permite que a página seja renderizada.
            return Page();
        }
    }
}