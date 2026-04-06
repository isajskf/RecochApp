using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecochApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "ModosJuego",
                columns: table => new
                {
                    IdModo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadJugadores = table.Column<int>(type: "int", nullable: false),
                    TiempoMinutos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModosJuego", x => x.IdModo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Contenidos",
                columns: table => new
                {
                    IdContenido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenidos", x => x.IdContenido);
                    table.ForeignKey(
                        name: "FK_Contenidos_Categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    IdSala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdAnfitrion = table.Column<int>(type: "int", nullable: false),
                    AnfitrionIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.IdSala);
                    table.ForeignKey(
                        name: "FK_Salas_Usuarios_AnfitrionIdUsuario",
                        column: x => x.AnfitrionIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ModosContenidos",
                columns: table => new
                {
                    IdModo = table.Column<int>(type: "int", nullable: false),
                    IdContenido = table.Column<int>(type: "int", nullable: false),
                    ModoJuegoIdModo = table.Column<int>(type: "int", nullable: true),
                    ContenidoIdContenido = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModosContenidos", x => new { x.IdModo, x.IdContenido });
                    table.ForeignKey(
                        name: "FK_ModosContenidos_Contenidos_ContenidoIdContenido",
                        column: x => x.ContenidoIdContenido,
                        principalTable: "Contenidos",
                        principalColumn: "IdContenido");
                    table.ForeignKey(
                        name: "FK_ModosContenidos_ModosJuego_ModoJuegoIdModo",
                        column: x => x.ModoJuegoIdModo,
                        principalTable: "ModosJuego",
                        principalColumn: "IdModo");
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdSala = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: true),
                    SalaIdSala = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.IdParticipante);
                    table.ForeignKey(
                        name: "FK_Participantes_Salas_SalaIdSala",
                        column: x => x.SalaIdSala,
                        principalTable: "Salas",
                        principalColumn: "IdSala");
                    table.ForeignKey(
                        name: "FK_Participantes_Usuarios_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    IdTurno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSala = table.Column<int>(type: "int", nullable: false),
                    IdParticipante = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaIdSala = table.Column<int>(type: "int", nullable: true),
                    ParticipanteIdParticipante = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.IdTurno);
                    table.ForeignKey(
                        name: "FK_Turnos_Participantes_ParticipanteIdParticipante",
                        column: x => x.ParticipanteIdParticipante,
                        principalTable: "Participantes",
                        principalColumn: "IdParticipante");
                    table.ForeignKey(
                        name: "FK_Turnos_Salas_SalaIdSala",
                        column: x => x.SalaIdSala,
                        principalTable: "Salas",
                        principalColumn: "IdSala");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contenidos_CategoriaIdCategoria",
                table: "Contenidos",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ModosContenidos_ContenidoIdContenido",
                table: "ModosContenidos",
                column: "ContenidoIdContenido");

            migrationBuilder.CreateIndex(
                name: "IX_ModosContenidos_ModoJuegoIdModo",
                table: "ModosContenidos",
                column: "ModoJuegoIdModo");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_SalaIdSala",
                table: "Participantes",
                column: "SalaIdSala");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_UsuarioIdUsuario",
                table: "Participantes",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_AnfitrionIdUsuario",
                table: "Salas",
                column: "AnfitrionIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_Codigo",
                table: "Salas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ParticipanteIdParticipante",
                table: "Turnos",
                column: "ParticipanteIdParticipante");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_SalaIdSala",
                table: "Turnos",
                column: "SalaIdSala");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModosContenidos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Contenidos");

            migrationBuilder.DropTable(
                name: "ModosJuego");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
