using System.ComponentModel.DataAnnotations;

namespace AgenciaTurismo.Models
{
    public class PaisDestino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do país é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O código ISO é obrigatório.")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "O código ISO deve ter entre 2 e 3 caracteres.")]
        [Display(Name = "Código ISO")]
        public string CodigoISO { get; set; }

        [Required(ErrorMessage = "O nome do continente é obrigatório.")]
        [StringLength(30)]
        public string Continente { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<CidadeDestino> Cidades { get; set; } = new List<CidadeDestino>();
    }
}