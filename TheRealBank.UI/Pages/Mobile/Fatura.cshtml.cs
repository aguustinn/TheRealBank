using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheRealBank.UI.Pages.Mobile
{
    // Modelo para um lançamento no cartão
    public class TransacaoCartao
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Icone { get; set; }
    }

    // Modelo para uma fatura antiga
    public class FaturaHistorico
    {
        public string MesAno { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } // "Paga", "Em aberto"
    }

    public class FaturaModel : PageModel
    {
        public decimal FaturaAtual { get; private set; }
        public decimal LimiteDisponivel { get; private set; }
        public decimal LimiteTotal { get; private set; }
        public DateTime Vencimento { get; private set; }
        public decimal PagamentoMinimo { get; private set; }
        public int PercentualUsado { get; private set; }

        public List<TransacaoCartao> LancamentosAtuais { get; private set; }
        public List<FaturaHistorico> FaturasAnteriores { get; private set; }

        public void OnGet()
        {
            // Puxando os dados do seu print da Home
            FaturaAtual = 1256.45m;
            LimiteDisponivel = 8987.00m;

            // Calculando os outros dados
            LimiteTotal = FaturaAtual + LimiteDisponivel;
            Vencimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28); // Dia 28
            PagamentoMinimo = FaturaAtual * 0.15m; // Simula 15%
            PercentualUsado = (int)Math.Round((double)(FaturaAtual / LimiteTotal) * 100);

            // Simula lançamentos da fatura atual
            LancamentosAtuais = new List<TransacaoCartao>
            {
                new TransacaoCartao { Descricao = "iFood", Data = DateTime.Now.AddDays(-2), Valor = 89.90m, Icone = "fas fa-utensils" },
                new TransacaoCartao { Descricao = "Spotify", Data = DateTime.Now.AddDays(-3), Valor = 34.90m, Icone = "fab fa-spotify" },
                new TransacaoCartao { Descricao = "Supermercado XYZ", Data = DateTime.Now.AddDays(-3), Valor = 310.50m, Icone = "fas fa-shopping-cart" },
                new TransacaoCartao { Descricao = "Posto Shell", Data = DateTime.Now.AddDays(-5), Valor = 150.00m, Icone = "fas fa-gas-pump" }
            };

            // Simula faturas passadas
            FaturasAnteriores = new List<FaturaHistorico>
            {
                new FaturaHistorico { MesAno = "Outubro 2025", ValorTotal = 980.20m, Status = "Paga" },
                new FaturaHistorico { MesAno = "Setembro 2025", ValorTotal = 1150.00m, Status = "Paga" },
                new FaturaHistorico { MesAno = "Agosto 2025", ValorTotal = 750.80m, Status = "Paga" },
            };
        }
    }
}