using AgenciaTurismo.Models;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Data
{
    public class AgenciaTurismoContext : DbContext
    {
        public AgenciaTurismoContext(DbContextOptions<AgenciaTurismoContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PaisDestino> PaisesDestino { get; set; }
        public DbSet<CidadeDestino> CidadesDestino { get; set; }
        public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<PacoteTuristicoDestino> PacoteTuristicoDestinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da tabela de junção muitos-para-muitos
            modelBuilder.Entity<PacoteTuristicoDestino>()
                .HasKey(pd => new { pd.PacoteTuristicoId, pd.CidadeDestinoId });

            modelBuilder.Entity<PacoteTuristicoDestino>()
                .HasOne(pd => pd.PacoteTuristico)
                .WithMany(p => p.PacoteTuristicoDestinos)
                .HasForeignKey(pd => pd.PacoteTuristicoId);

            modelBuilder.Entity<PacoteTuristicoDestino>()
                .HasOne(pd => pd.CidadeDestino)
                .WithMany(c => c.PacoteTuristicoDestinos)
                .HasForeignKey(pd => pd.CidadeDestinoId);

            // Configuração de relacionamento um-para-muitos
            modelBuilder.Entity<CidadeDestino>()
                .HasOne(c => c.PaisDestino)
                .WithMany(p => p.Cidades)
                .HasForeignKey(c => c.PaisDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.PacoteTuristico)
                .WithMany(p => p.Reservas)
                .HasForeignKey(r => r.PacoteTuristicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração de precisao decimal
            modelBuilder.Entity<PacoteTuristico>()
                .Property(p => p.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Reserva>()
                .Property(r => r.ValorTotal)
                .HasPrecision(10, 2);

            // Índices par melhor performance
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            // Filtros globais para exclusao lógica
            modelBuilder.Entity<Cliente>()
                .HasQueryFilter(c => !c.DeletedAt.HasValue);

            modelBuilder.Entity<PaisDestino>()
                .HasQueryFilter(p => !p.DeletedAt.HasValue);

            modelBuilder.Entity<CidadeDestino>()
                .HasQueryFilter(c => !c.DeletedAt.HasValue);

            modelBuilder.Entity<PacoteTuristico>()
                .HasQueryFilter(p => !p.DeletedAt.HasValue);

            modelBuilder.Entity<Reserva>()
                .HasQueryFilter(r => !r.DeletedAt.HasValue);

            // Seed de dados iniciais
            modelBuilder.Entity<PaisDestino>().HasData(
                new PaisDestino { Id = 1, Nome = "Brasil", CodigoISO = "BR", Continente = "América do Sul" },
                new PaisDestino { Id = 2, Nome = "Estados Unidos", CodigoISO = "US", Continente = "América do Norte" },
                new PaisDestino { Id = 3, Nome = "França", CodigoISO = "FR", Continente = "Europa" }
            );

            modelBuilder.Entity<CidadeDestino>().HasData(
                new CidadeDestino { Id = 1, Nome = "Rio de Janeiro", Estado = "RJ", PaisDestinoId = 1, Descricao = "Cidade Maravilhosa" },
                new CidadeDestino { Id = 2, Nome = "São Paulo", Estado = "SP", PaisDestinoId = 1, Descricao = "Capital financeira do Brasil" },
                new CidadeDestino { Id = 3, Nome = "New York", Estado = "NY", PaisDestinoId = 2, Descricao = "A cidade que nunca dorme" },
                new CidadeDestino { Id = 4, Nome = "Paris", Estado = "Île-de-France", PaisDestinoId = 3, Descricao = "Cidade Luz" }
            );
        }
    }
}