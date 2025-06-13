using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Cidades
{
    public class IndexModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public IndexModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public IList<CidadeDestino> CidadeDestino { get; set; } = default!;

        public async Task OnGetAsync()
        {
            CidadeDestino = await _context.CidadesDestino
                .Include(c => c.PaisDestino).ToListAsync();
        }
    }
}