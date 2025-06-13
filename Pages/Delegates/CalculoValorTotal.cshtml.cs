using AgenciaTurismo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages.Delegates
{
    public class CalculoValorTotalModel : PageModel
    {
        [BindProperty]
        public int NumeroDiarias { get; set; }

        [BindProperty]
        public decimal ValorDiaria { get; set; }

        public decimal? ValorTotal { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Usando Func com express�o lambda
                ValorTotal = DelegateServices.CalcularValorTotal(NumeroDiarias, ValorDiaria);

                // Log
                LogDelegate logger = DelegateServices.LogToMemory;
                logger($"C�lculo de valor total: {NumeroDiarias} di�rias x R$ {ValorDiaria:F2} = R$ {ValorTotal:F2}");
            }

            return Page();
        }
    }
}