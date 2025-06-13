using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgenciaTurismo.Pages.Notes
{
    public class ViewNotesModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ViewNotesModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public string TituloNota { get; set; }

        [BindProperty]
        public string ConteudoNota { get; set; }

        public List<string> ArquivosDisponiveis { get; set; } = new List<string>();
        public string ArquivoSelecionado { get; set; }
        public string ConteudoArquivo { get; set; }

        public void OnGet(string arquivo = null)
        {
            CarregarArquivos();

            if (!string.IsNullOrEmpty(arquivo))
            {
                LerArquivo(arquivo);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CarregarArquivos();
                return Page();
            }

            // Criar diretório se não existir
            var filesPath = Path.Combine(_environment.WebRootPath, "files");
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            // Gerar nome único para o arquivo
            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{TituloNota.Replace(" ", "_")}.txt";
            var filePath = Path.Combine(filesPath, fileName);

            // Salvar o arquivo
            await System.IO.File.WriteAllTextAsync(filePath, ConteudoNota);

            TempData["SuccessMessage"] = "Nota salva com sucesso!";
            return RedirectToPage();
        }

        private void CarregarArquivos()
        {
            var filesPath = Path.Combine(_environment.WebRootPath, "files");
            if (Directory.Exists(filesPath))
            {
                ArquivosDisponiveis = Directory.GetFiles(filesPath, "*.txt")
                    .Select(Path.GetFileName)
                    .OrderByDescending(f => f)
                    .ToList();
            }
        }

        private void LerArquivo(string nomeArquivo)
        {
            var filesPath = Path.Combine(_environment.WebRootPath, "files");
            var filePath = Path.Combine(filesPath, nomeArquivo);

            if (System.IO.File.Exists(filePath))
            {
                ArquivoSelecionado = nomeArquivo;
                ConteudoArquivo = System.IO.File.ReadAllText(filePath);
            }
        }
    }
}