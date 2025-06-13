using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Cidades
{
    public class DeleteModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public DeleteModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CidadeDestino CidadeDestino { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            bool cidadeEmUso = await _context.PacoteTuristicoDestinos.AnyAsync(pd => pd.CidadeDestinoId == id);
            if (cidadeEmUso)
            {
                ModelState.AddModelError(string.Empty, "Esta cidade não pode ser excluída pois já faz parte de um ou mais pacotes turísticos.");

                CidadeDestino = await _context.CidadesDestino.Include(c => c.PaisDestino).FirstOrDefaultAsync(m => m.Id == id);
                if (CidadeDestino == null) return NotFound();

                return Page();
            }

            var cidadedestino = await _context.CidadesDestino.FindAsync(id);
            if (cidadedestino != null)
            {
                cidadedestino.DeletedAt = DateTime.UtcNow;
                _context.Update(cidadedestino);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}