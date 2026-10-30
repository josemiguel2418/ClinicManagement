namespace ClinicManagement.Backend.Domain.Entities
{
    public class SignosVitales
    {
        public int Id { get; set; }

        public int ConsultaId { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Peso { get; set; }
        public int? FrecuenciaCardiaca { get; set; }
        public decimal? Temperatura { get; set; }
        public int? PresionSistolica { get; set; }
        public int? PresionDiastolica { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Consulta Consulta { get; set; } = null!;
    }
}
