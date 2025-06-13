using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Clientes
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
            return Page();
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {
                return Page(); 
            }


            // Verificação de CPF duplicado
            bool cpfJaExiste = await _context.Clientes
                                             .IgnoreQueryFilters()
                                             .AnyAsync(c => c.CPF == Cliente.CPF);

            if (cpfJaExiste)
            {
                ModelState.AddModelError("Cliente.CPF", "Este CPF já está cadastrado no sistema.");
            }

            // Verificação de E-mail duplicado
            bool emailJaExiste = await _context.Clientes
                                               .IgnoreQueryFilters()
                                               .AnyAsync(c => c.Email == Cliente.Email);

            if (emailJaExiste)
            {
                ModelState.AddModelError("Cliente.Email", "Este e-mail já está cadastrado no sistema.");
            }

            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Clientes.Add(Cliente);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}