using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TheRealBank.UI.Pages.Mobile.Pay
{
    // Modelo para simular uma Chave PIX
    public class PixKey
    {
        public string Id { get; set; } // Um ID único para o radio button
        public string Tipo { get; set; } // "CPF", "E-mail", "Celular", "Aleatória"
        public string Valor { get; set; }
        public string Icone { get; set; }
    }

    public class KeysModel : PageModel
    {
        // Lista de chaves que o usuário possui
        public List<PixKey> ChavesPix { get; set; }

        // Propriedade para receber a seleção do usuário
        [BindProperty]
        [Required]
        public string SelectedKeyId { get; set; }

        public void OnGet()
        {
            // Simula as chaves cadastradas
            ChavesPix = new List<PixKey>
            {
                new PixKey
                {
                    Id = "1",
                    Tipo = "CPF",
                    Valor = "***.123.456-**",
                    Icone = "fas fa-user-lock"
                },
                new PixKey
                {
                    Id = "2",
                    Tipo = "E-mail",
                    Valor = "g******l@email.com",
                    Icone = "fas fa-envelope"
                },
                new PixKey
                {
                    Id = "3",
                    Tipo = "Chave Aleatória",
                    Valor = "a1b2c3d4-e5f6-...",
                    Icone = "fas fa-key"
                }
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Se nada foi selecionado, recarrega a página
                OnGet();
                return Page();
            }

            // LÓGICA DEPOIS DE SELECIONAR:
            // O usuário selecionou a chave com ID = SelectedKeyId
            // O próximo passo lógico é redirecionar para a página "Receber",
            // passando a chave que ele escolheu.

            // Exemplo:
            // return RedirectToPage("/Mobile/Receber", new { chave = SelectedKeyId });

            // Por enquanto, vamos apenas voltar para a Home
            return RedirectToPage("/Experiencia/Layout");
        }
    }
}