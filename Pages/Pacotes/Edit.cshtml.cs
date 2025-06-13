using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using AgenciaTurismo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Pacotes
{
    public class EditModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public EditModel(AgenciaTurismoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PacoteTuristico PacoteTuristico { get; set; } = default!;
        public List<AssignedCidadeData> AssignedCidadesDataList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            PacoteTuristico = await _context.PacotesTuristicos
                .Include(p => p.PacoteTuristicoDestinos) // Carrega as associações atuais
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (PacoteTuristico == null) return NotFound();

            PopulateAssignedCidadeData(_context, PacoteTuristico);
            return Page();
        }

        public void PopulateAssignedCidadeData(AgenciaTurismoContext context, PacoteTuristico pacote)
        {
            var todasCidades = context.CidadesDestino;
            var pacoteCidades = new HashSet<int>(pacote.PacoteTuristicoDestinos.Select(c => c.CidadeDestinoId));
            AssignedCidadesDataList = new List<AssignedCidadeData>();
            foreach (var cidade in todasCidades)
            {
                AssignedCidadesDataList.Add(new AssignedCidadeData
                {
                    CidadeID = cidade.Id,
                    Nome = cidade.Nome,
                    Assigned = pacoteCidades.Contains(cidade.Id)
                });
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCidades)
        {
            if (id == null) return NotFound();

            var pacoteToUpdate = await _context.PacotesTuristicos
                .Include(p => p.PacoteTuristicoDestinos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacoteToUpdate == null) return NotFound();

            if (await TryUpdateModelAsync<PacoteTuristico>(
                pacoteToUpdate,
                "PacoteTuristico",
                 p => p.Titulo, p => p.Descricao, p => p.Inclusoes, p => p.DataInicio, p => p.DuracaoDias, p => p.CapacidadeMaxima, p => p.Preco))
            {
                UpdatePacoteCidades(selectedCidades, pacoteToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCidadeData(_context, pacoteToUpdate);
            return Page();
        }

        public void UpdatePacoteCidades(string[] selectedCidades, PacoteTuristico pacoteToUpdate)
        {
            if (selectedCidades == null)
            {
                pacoteToUpdate.PacoteTuristicoDestinos = new List<PacoteTuristicoDestino>();
                return;
            }

            var selectedCidadesHS = new HashSet<string>(selectedCidades);
            var pacoteCidades = new HashSet<int>(pacoteToUpdate.PacoteTuristicoDestinos.Select(c => c.CidadeDestinoId));

            foreach (var cidade in _context.CidadesDestino)
            {
                if (selectedCidadesHS.Contains(cidade.Id.ToString()))
                {
                    if (!pacoteCidades.Contains(cidade.Id))
                    {
                        pacoteToUpdate.PacoteTuristicoDestinos.Add(new PacoteTuristicoDestino { PacoteTuristicoId = pacoteToUpdate.Id, CidadeDestinoId = cidade.Id });
                    }
                }
                else
                {
                    if (pacoteCidades.Contains(cidade.Id))
                    {
                        var cidadeToRemove = pacoteToUpdate.PacoteTuristicoDestinos.FirstOrDefault(c => c.CidadeDestinoId == cidade.Id);
                        if (cidadeToRemove != null) _context.Remove(cidadeToRemove);
                    }
                }
            }
        }
    }
}