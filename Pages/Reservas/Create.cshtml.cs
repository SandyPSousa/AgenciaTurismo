using AgenciaTurismo.Data;
using AgenciaTurismo.Models;
using AgenciaTurismo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Pages.Reservas
{
    public class CreateModel : PageModel
    {
        private readonly AgenciaTurismoContext _context;

        public CreateModel(AgenciaTurismoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateDropdownsAsync();
            return Page();
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            var pacote = await _context.PacotesTuristicos.Include(p => p.Reservas).FirstOrDefaultAsync(p => p.Id == Reserva.PacoteTuristicoId);

            Reserva.DataReserva = DateTime.UtcNow;
            Reserva.ValorTotal = pacote.Preco * Reserva.NumeroPessoas;
            Reserva.Status = StatusReserva.Confirmada;

            DelegateServices.SubscribeToCapacityEvent(pacote);
            pacote.AdicionarReserva(Reserva);

            await _context.SaveChangesAsync();

          
            LogDelegate logger = DelegateServices.LogToConsole;
            logger += DelegateServices.LogToFile;
            logger += DelegateServices.LogToMemory;

            string logMessage = $"Nova reserva criada com sucesso. Cliente ID: {Reserva.ClienteId}, Pacote ID: {Reserva.PacoteTuristicoId}, Pessoas: {Reserva.NumeroPessoas}.";

            
            logger(logMessage);

            return RedirectToPage("./Index");
        }

        private async Task PopulateDropdownsAsync()
        {
            ViewData["Clientes"] = new SelectList(await _context.Clientes.OrderBy(c => c.Nome).ToListAsync(), "Id", "Nome");

            var pacotes = await _context.PacotesTuristicos.Include(p => p.Reservas).ToListAsync();
            var pacotesDisponiveis = pacotes.Where(p => p.EstaDisponivel).OrderBy(p => p.Titulo);

            ViewData["Pacotes"] = new SelectList(pacotesDisponiveis, "Id", "Titulo");
        }
    }
}