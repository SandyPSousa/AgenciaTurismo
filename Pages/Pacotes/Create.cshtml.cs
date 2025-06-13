using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using AgenciaTurismo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Pacotes
{
    public class CreateModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public CreateModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; }
        public List<AssignedCidadeData> AssignedCidadesDataList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AssignedCidadesDataList = new List<AssignedCidadeData>();
            var cidades = await _context.CidadesDestino.ToListAsync();
            foreach (var cidade in cidades)
            {
                AssignedCidadesDataList.Add(new AssignedCidadeData
                {
                    CidadeID = cidade.Id,
                    Nome = cidade.Nome,
                    Assigned = false
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCidades)
        {
            var newPacote = new PacoteTuristico();

            if (await TryUpdateModelAsync<PacoteTuristico>(
                newPacote,
                "PacoteTuristico",
                 p => p.Titulo, p => p.Descricao, p => p.DataInicio, p => p.DuracaoDias, p => p.CapacidadeMaxima, p => p.Preco))
            {
                if (selectedCidades != null)
                {
                    foreach (var cidadeId in selectedCidades)
                    {
                        var cidadeToAdd = new PacoteTuristicoDestino
                        {
                            PacoteTuristico = newPacote,
                            CidadeDestinoId = int.Parse(cidadeId)
                        };
                        _context.PacoteTuristicoDestinos.Add(cidadeToAdd);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}