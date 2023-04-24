
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contextos
{
    // Caso não seja definido esses parametros o Identity vai ignorar as tabelas criadas por nos, User, Role, UserRoles
    public class ProEventosContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
                                                       IdentityUserRole<int>, IdentityUserLogin<int>, 
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
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
                        .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            
            // Configura a Deleção do EVENTO em CASCATA de dados relacionados na tabela RedeSociais
            
            modelBuilder.Entity<Evento>()
                        .HasMany(ev => ev.RedesSociais)
                        .WithOne(rs => rs.Evento)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                         .HasMany(e => e.RedeSociais) // palestrante vc tem varias redes sociais
                         .WithOne(rs => rs.Palestrante) // cada rede social tem um palestrante 
                         .OnDelete(DeleteBehavior.Cascade);

        }    }

}




