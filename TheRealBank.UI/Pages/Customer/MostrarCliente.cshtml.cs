// Pages/Customer/MostrarCliente.cshtml.cs

using Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
// Adicione os 'usings' necessários

namespace TheRealBank.UI.Pages.Customer
{
    public class MostrarClienteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        // Propriedade para guardar os dados do cliente que serão exibidos
        public Customers.Customer Cliente { get; set; }

        // [BindProperty] com SupportsGet=true conecta esta propriedade com o parâmetro 'id' na URL.
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public MostrarClienteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // OnGetAsync é executado quando a página é carregada
        public async Task<IActionResult> OnGetAsync()
        {
            // Usa o 'Id' (CPF) recebido da URL para buscar o cliente
            Cliente = await _customerService.GetCustomerByCpfAsync(Id);

            // Se nenhum cliente for encontrado com esse CPF, redireciona para uma página de "Não Encontrado"
            if (Cliente == null)
            {
                return NotFound();
            }

            // Se encontrou, permite que a página seja renderizada
            return Page();
        }
    }
}