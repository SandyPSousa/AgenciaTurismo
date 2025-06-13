using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Cidades
{
    public class EditModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public EditModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CidadeDestino CidadeDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var cidadedestino = await _context.CidadesDestino.FirstOrDefaultAsync(m => m.Id == id);
            if (cidadedestino == null) return NotFound();

            CidadeDestino = cidadedestino;

            ViewData["Paises"] = new SelectList(_context.PaisesDestino, "Id", "Nome");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("CidadeDestino.PaisDestino");

            if (!ModelState.IsValid)
            {
                ViewData["Paises"] = new SelectList(_context.PaisesDestino, "Id", "Nome");
                return Page();
            }

            _context.Attach(CidadeDestino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeDestinoExists(CidadeDestino.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CidadeDestinoExists(int id)
        {
            return _context.CidadesDestino.Any(e => e.Id == id);
        }
    }
}