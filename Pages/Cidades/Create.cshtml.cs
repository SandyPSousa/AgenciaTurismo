using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AgenciaTurismo.Pages.Cidades
{
    public class CreateModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public CreateModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Paises"] = new SelectList(_context.PaisesDestino, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public CidadeDestino CidadeDestino { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("CidadeDestino.PaisDestino");

            if (!ModelState.IsValid)
            {
                ViewData["Paises"] = new SelectList(_context.PaisesDestino, "Id", "Nome");
                return Page();
            }

            _context.CidadesDestino.Add(CidadeDestino);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}