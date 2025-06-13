using AgenciaTurismo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages.Delegates
{
    public class CalculoDescontoModel : PageModel
    {
        [BindProperty]
        public decimal ValorOriginal { get; set; }

        [BindProperty]
        public decimal PercentualDesconto { get; set; } = 10; // Padr�o 10%

        public decimal? ValorComDesconto { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Usando o delegate
                CalculateDelegate calcular = DelegateServices.CalcularDesconto;
                ValorComDesconto = calcular(ValorOriginal, PercentualDesconto);

                // Log da opera��o
                LogDelegate logger = DelegateServices.LogToConsole;
                logger += DelegateServices.LogToFile;
                logger += DelegateServices.LogToMemory;

                logger($"C�lculo de desconto realizado: R$ {ValorOriginal:F2} - {PercentualDesconto}% = R$ {ValorComDesconto:F2}");
            }

            return Page();
        }
    }
}