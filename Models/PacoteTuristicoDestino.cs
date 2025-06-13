namespace AgenciaTurismo.Models
{
    public class PacoteTuristicoDestino
    {
        public int PacoteTuristicoId { get; set; }
        public PacoteTuristico? PacoteTuristico { get; set; }

        public int CidadeDestinoId { get; set; }
        public CidadeDestino? CidadeDestino { get; set; }

        public int OrdemVisita { get; set; }
    }
}