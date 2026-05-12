namespace RecochApp.API.Dtos.Salas
{
    public class SalaResponse
    {
        // Propiedades que representan la información de una sala, incluyendo su ID, código, anfitrión, modo, estado, número máximo de participantes, duración en minutos y cantidad actual de participantes.
        public int IdSala { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int IdAnfitrion { get; set; }
        public int IdModo { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int MaxParticipantes { get; set; }
        public int? DuracionMinutos { get; set; }
        public int CantidadParticipantes { get; set; }
    }
}
