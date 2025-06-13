using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgenciaTurismo.Models
{
    public class CidadeDestino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da cidade é obrigatório.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 60 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome do estado é obrigatório.")]
        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "É obrigatório selecionar um país.")]
        [Display(Name = "País")]
        public int PaisDestinoId { get; set; }

        [ForeignKey("PaisDestinoId")]
        public PaisDestino? PaisDestino { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<PacoteTuristicoDestino> PacoteTuristicoDestinos { get; set; } = new List<PacoteTuristicoDestino>();
    }
}