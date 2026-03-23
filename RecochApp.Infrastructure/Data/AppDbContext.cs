using Microsoft.EntityFrameworkCore;
using RecochApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RecochApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<ModoJuego> ModosJuego { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Contenido> Contenidos { get; set; }
        public DbSet<ModoContenido> ModosContenidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Claves primarias explícitas
            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.IdCategoria);

            modelBuilder.Entity<Contenido>()
                .HasKey(c => c.IdContenido);

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Sala>()
                .HasKey(s => s.IdSala);

            modelBuilder.Entity<Participante>()
                .HasKey(p => p.IdParticipante);

            modelBuilder.Entity<Turno>()
                .HasKey(t => t.IdTurno);

            modelBuilder.Entity<ModoJuego>()
                .HasKey(m => m.IdModo);

            // Clave primaria compuesta de ModoContenido
            modelBuilder.Entity<ModoContenido>()
                .HasKey(mc => new { mc.IdModo, mc.IdContenido });

            // Índices únicos
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Sala>()
                .HasIndex(s => s.Codigo)
                .IsUnique();
        }
    }
}
