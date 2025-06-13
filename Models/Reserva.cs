using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenciaTurismo.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um cliente.")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um pacote.")]
        [Display(Name = "Pacote Turístico")]
        public int PacoteTuristicoId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data da Reserva")]
        public DateTime DataReserva { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Valor Pago")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "O número de pessoas é obrigatório.")]
        [Range(1, 10, ErrorMessage = "A reserva deve ser para no mínimo 1 e no máximo 10 pessoas.")]
        [Display(Name = "Número de Pessoas")]
        public int NumeroPessoas { get; set; }

        public StatusReserva Status { get; set; }

        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("PacoteTuristicoId")]
        public PacoteTuristico? PacoteTuristico { get; set; }
    }
}