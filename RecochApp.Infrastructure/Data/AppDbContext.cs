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
            // USUARIO
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).HasColumnName("correo");
            modelBuilder.Entity<Usuario>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaNacimiento).HasColumnName("fecha_nacimiento");

            //CATEGORIA
            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasColumnName("id_categoria");
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).HasColumnName("nombre");

            // CONTENIDO
            modelBuilder.Entity<Contenido>().HasKey(c => c.IdContenido);
            modelBuilder.Entity<Contenido>().Property(c => c.IdContenido).HasColumnName("id_contenido");
            modelBuilder.Entity<Contenido>().Property(c => c.Texto).HasColumnName("texto");
            modelBuilder.Entity<Contenido>().Property(c => c.Tipo).HasColumnName("tipo");
            modelBuilder.Entity<Contenido>().Property(c => c.IdCategoria).HasColumnName("id_categoria");

            // SALA
            modelBuilder.Entity<Sala>().HasKey(s => s.IdSala);
            modelBuilder.Entity<Sala>().Property(s => s.IdSala).HasColumnName("id_sala");
            modelBuilder.Entity<Sala>().Property(s => s.Codigo).HasColumnName("codigo");
            modelBuilder.Entity<Sala>().Property(s => s.IdAnfitrion).HasColumnName("id_anfitrion");

            // PARTICIPANTE
            modelBuilder.Entity<Participante>().HasKey(p => p.IdParticipante);
            modelBuilder.Entity<Participante>().Property(p => p.IdParticipante).HasColumnName("id_participante");
            modelBuilder.Entity<Participante>().Property(p => p.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Participante>().Property(p => p.IdSala).HasColumnName("id_sala");

            // TURNO
            modelBuilder.Entity<Turno>().HasKey(t => t.IdTurno);
            modelBuilder.Entity<Turno>().Property(t => t.IdTurno).HasColumnName("id_turno");
            modelBuilder.Entity<Turno>().Property(t => t.IdSala).HasColumnName("id_sala");
            modelBuilder.Entity<Turno>().Property(t => t.IdParticipante).HasColumnName("id_participante");
            modelBuilder.Entity<Turno>().Property(t => t.
            Orden).HasColumnName("orden");
            modelBuilder.Entity<Turno>().Property(t => t.Estado).HasColumnName("estado");

            // MODO JUEGO
            modelBuilder.Entity<ModoJuego>().HasKey(m => m.IdModo);
            modelBuilder.Entity<ModoJuego>().Property(m => m.IdModo).HasColumnName("id_modo");
            modelBuilder.Entity<ModoJuego>().Property(m => m.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<ModoJuego>().Property(m => m.CantidadJugadores).HasColumnName("cantidad_jugadores");
            modelBuilder.Entity<ModoJuego>().Property(m => m.TiempoMinutos).HasColumnName("tiempo_minutos");

            // MUCHOS A MUCHOS
           

            modelBuilder.Entity<ModoContenido>()
                .HasKey(mc => new { mc.IdModo, mc.IdContenido });

            modelBuilder.Entity<ModoContenido>().Property(mc => mc.IdModo).HasColumnName("id_modo");
            modelBuilder.Entity<ModoContenido>().Property(mc => mc.IdContenido).HasColumnName("id_contenido");
        }
    }
}
