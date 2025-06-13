using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenciaTurismo.Models
{
    public class PacoteTuristico
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Inclusoes { get; set; }
        public DateTime DataInicio { get; set; }
        public int DuracaoDias { get; set; }
        public int CapacidadeMaxima { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<PacoteTuristicoDestino> PacoteTuristicoDestinos { get; set; } = new List<PacoteTuristicoDestino>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

       
        public event Action<string>? CapacityReached;

        public void AdicionarReserva(Reserva reserva)
        {
            this.Reservas.Add(reserva);

            if (this.VagasDisponiveis <= 0)
            {
               
                CapacityReached?.Invoke($"O pacote '{this.Titulo}' (ID: {this.Id}) atingiu sua capacidade máxima.");
            }
        }


        [NotMapped]
        [Display(Name = "Data de Término")]
        public DateTime DataTermino => DataInicio.AddDays(DuracaoDias);

        [NotMapped]
        public int VagasOcupadas => Reservas.Where(r => r.DeletedAt == null).Sum(r => r.NumeroPessoas);

        [NotMapped]
        public int VagasDisponiveis => CapacidadeMaxima - VagasOcupadas;

        [NotMapped]
        public bool EstaDisponivel => VagasDisponiveis > 0 && DataInicio.Date >= DateTime.Now.Date;
    }
}