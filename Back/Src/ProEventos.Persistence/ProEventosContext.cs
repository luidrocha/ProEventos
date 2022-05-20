
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;


namespace ProEventos.Persistence
{
    public class ProEventosContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        
            
            public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }


            // Determina a classe de junção PalestranteEvento / Tabela auxiliar
            // Sobrepoe o metodo padrão OnModelCreating que define o mapeamento das tabelas
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                
                modelBuilder.Entity<PalestranteEvento>()
                            .HasKey(PE => new {PE.EventoId, PE.PalestranteId});
                
               }

           
    }

}




