using Microsoft.EntityFrameworkCore;
using RecochApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RecochApp.Infrastructure.Data
{
    // Clase que representa el contexto de la base de datos para la aplicación, utilizando Entity Framework Core. Esta clase define las entidades y sus relaciones, así como la configuración de la base de datos.
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración para el contexto de la base de datos y las pasa a la clase base DbContext.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<ModoJuego> ModosJuego { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Contenido> Contenidos { get; set; }
        public DbSet<ModoContenido> ModosContenidos { get; set; }
        public DbSet<Castigo> Castigos { get; set; }
        public DbSet<TurnoCastigo> TurnoCastigo { get; set; }

        // Método que se llama al crear el modelo de la base de datos. Aquí se configuran las entidades, sus propiedades y las relaciones entre ellas utilizando Fluent API.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USUARIO
            // Configuración de la entidad Usuario, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).HasColumnName("correo");
            modelBuilder.Entity<Usuario>().Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<Usuario>().Property(u => u.FechaNacimiento).HasColumnName("fecha_nacimiento");

            // CATEGORIA
            // Configuración de la entidad Categoria, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasColumnName("id_categoria");
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).HasColumnName("nombre");

            // CONTENIDO
            // Configuración de la entidad Contenido, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<Contenido>().HasKey(c => c.IdContenido);
            modelBuilder.Entity<Contenido>().Property(c => c.IdContenido).HasColumnName("id_contenido");
            modelBuilder.Entity<Contenido>().Property(c => c.Texto).HasColumnName("texto");
            modelBuilder.Entity<Contenido>().Property(c => c.Tipo).HasColumnName("tipo");
            modelBuilder.Entity<Contenido>().Property(c => c.IdCategoria).HasColumnName("id_categoria");

            // SALA
            // Configuración de la entidad Sala, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<Sala>().HasKey(s => s.IdSala);
            modelBuilder.Entity<Sala>().Property(s => s.IdSala).HasColumnName("id_sala");
            modelBuilder.Entity<Sala>().Property(s => s.Codigo).HasColumnName("codigo");
            modelBuilder.Entity<Sala>().Property(s => s.IdAnfitrion).HasColumnName("id_anfitrion");
            modelBuilder.Entity<Sala>().Property(s => s.IdModo).HasColumnName("id_modo");
            modelBuilder.Entity<Sala>().Property(s => s.Estado).HasColumnName("estado");
            modelBuilder.Entity<Sala>().Property(s => s.MaxParticipantes).HasColumnName("max_participantes");
            modelBuilder.Entity<Sala>().Property(s => s.DuracionMinutos).HasColumnName("duracion_minutos");

            // PARTICIPANTE
            modelBuilder.Entity<Participante>().HasKey(p => p.IdParticipante);
            modelBuilder.Entity<Participante>().Property(p => p.IdParticipante).HasColumnName("id_participante");
            modelBuilder.Entity<Participante>().Property(p => p.IdUsuario).HasColumnName("id_usuario");
            modelBuilder.Entity<Participante>().Property(p => p.IdSala).HasColumnName("id_sala");

            modelBuilder.Entity<Participante>()
                .HasIndex(p => new { p.IdSala, p.IdUsuario }).IsUnique();

            modelBuilder.Entity<Participante>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Participante>()
                .HasOne(p => p.Sala)
                .WithMany()              
                .HasForeignKey(p => p.IdSala)
                .OnDelete(DeleteBehavior.Cascade);

            // TURNO
            // Configuración de la entidad Turno, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<Turno>().HasKey(t => t.IdTurno);
            modelBuilder.Entity<Turno>().Property(t => t.IdTurno).HasColumnName("id_turno");
            modelBuilder.Entity<Turno>().Property(t => t.IdSala).HasColumnName("id_sala");
            modelBuilder.Entity<Turno>().Property(t => t.IdParticipante).HasColumnName("id_participante");
            modelBuilder.Entity<Turno>().Property(t => t.Orden).HasColumnName("orden");
            modelBuilder.Entity<Turno>().Property(t => t.Resultado).HasColumnName("resultado");

            // MODO JUEGO
            // Configuración de la entidad ModoJuego, estableciendo la clave primaria, los nombres de las columnas y las propiedades.
            modelBuilder.Entity<ModoJuego>().HasKey(m => m.IdModo);
            modelBuilder.Entity<ModoJuego>().Property(m => m.IdModo).HasColumnName("id_modo");
            modelBuilder.Entity<ModoJuego>().Property(m => m.Nombre).HasColumnName("nombre");
            modelBuilder.Entity<ModoJuego>().Property(m => m.CantidadJugadores).HasColumnName("cantidad_jugadores");
            modelBuilder.Entity<ModoJuego>().Property(m => m.TiempoMinutos).HasColumnName("tiempo_minutos");

            // MUCHOS A MUCHOS
            // Configuración de la relación muchos a muchos entre ModoJuego y Contenido a través de la entidad ModoContenido. Se establece una clave compuesta para la entidad ModoContenido utilizando las propiedades IdModo e IdContenido.
            modelBuilder.Entity<ModoContenido>()
                .HasKey(mc => new { mc.IdModo, mc.IdContenido });

            modelBuilder.Entity<ModoContenido>().Property(mc => mc.IdModo).HasColumnName("id_modo");
            modelBuilder.Entity<ModoContenido>().Property(mc => mc.IdContenido).HasColumnName("id_contenido");
        }
    }
}
