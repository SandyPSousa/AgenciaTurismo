using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Pacotes
{
    public class DetailsModel : PageModel
    {
        private readonly AgenciaTurismo.Data.AgenciaTurismoContext _context;

        public DetailsModel(AgenciaTurismo.Data.AgenciaTurismoContext context)
        {
            _context = context;
        }

        public PacoteTuristico PacoteTuristico { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PacoteTuristico = await _context.PacotesTuristicos
                .Include(p => p.PacoteTuristicoDestinos)
                .ThenInclude(pd => pd.CidadeDestino)
                .ThenInclude(c => c.PaisDestino)
                .Include(p => p.Reservas)
                .ThenInclude(r => r.Cliente)
                .AsNoTracking() 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (PacoteTuristico == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}