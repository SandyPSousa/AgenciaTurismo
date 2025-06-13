using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Paises
{
    public class DeleteModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public DeleteModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaisDestino PaisDestino { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var paisdestino = await _context.PaisesDestino.FirstOrDefaultAsync(m => m.Id == id);

            if (paisdestino == null) return NotFound();

            PaisDestino = paisdestino;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var paisdestino = await _context.PaisesDestino.FindAsync(id);
            if (paisdestino != null)
            {
                paisdestino.DeletedAt = DateTime.UtcNow;
                _context.Update(paisdestino);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}