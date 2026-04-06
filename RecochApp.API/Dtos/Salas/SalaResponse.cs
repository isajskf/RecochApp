namespace RecochApp.API.Dtos.Salas
{
    public class SalaResponse
    {
        public int IdSala { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int IdAnfitrion { get; set; }
        public int CantidadParticipantes { get; set; }
    }
}
