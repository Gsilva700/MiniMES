using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Models;

namespace MiniMES.Web.Data
{
    public class MiniMesContext : DbContext
    {
        public MiniMesContext(DbContextOptions<MiniMesContext> options) : base(options) { }

        public DbSet<Turno> Turnos => Set<Turno>();
        public DbSet<Operador> Operadores => Set<Operador>();
        public DbSet<Maquina> Maquinas => Set<Maquina>();
        public DbSet<Interrupcao> Interrupcoes => Set<Interrupcao>();
        public DbSet<OrdemProducao> OrdensProducao => Set<OrdemProducao>();
        public DbSet<Apontamento> Apontamentos => Set<Apontamento>();
        public DbSet<Parada> Paradas => Set<Parada>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operador>().HasIndex(o => o.Matricula).IsUnique();
            modelBuilder.Entity<Maquina>().HasIndex(m => m.Codigo).IsUnique();
            modelBuilder.Entity<OrdemProducao>().HasIndex(o => o.Numero).IsUnique();

            modelBuilder.Entity<Operador>()
                .HasOne(o => o.Turno).WithMany()
                .HasForeignKey(o => o.TurnoId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdemProducao>()
                .HasOne(o => o.Maquina).WithMany()
                .HasForeignKey(o => o.MaquinaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apontamento>()
                .HasOne(a => a.OrdemProducao).WithMany()
                .HasForeignKey(a => a.OrdemProducaoId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apontamento>()
                .HasOne(a => a.Maquina).WithMany()
                .HasForeignKey(a => a.MaquinaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Apontamento>()
                .HasOne(a => a.Operador).WithMany()
                .HasForeignKey(a => a.OperadorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parada>()
                .HasOne(p => p.Maquina).WithMany()
                .HasForeignKey(p => p.MaquinaId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parada>()
                .HasOne(p => p.Interrupcao).WithMany()
                .HasForeignKey(p => p.InterrupcaoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}