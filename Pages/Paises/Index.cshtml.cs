using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Paises
{
    public class IndexModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public IndexModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        public IList<PaisDestino> PaisDestino { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // O filtro de exclusão lógica já é aplicado globalmente pelo DbContext
            PaisDestino = await _context.PaisesDestino.ToListAsync();
        }
    }
}