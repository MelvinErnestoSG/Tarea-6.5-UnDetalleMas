using GestionPersonas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestionPersonas.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<TiposAportes> TiposAportes { get; set; }
        public DbSet<Aportes> Aportes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\PeopleGestor.db");
        }
    }
}
