using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Domain.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public int CitaId { get; set; }
        public string? Sintomas { get; set; }
        public string? Diagnostico { get; set; }
        public string? Observacion { get; set; }
        public string? Tratamiento { get; set; }
        public EstadoConsulta Estado { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public Cita Cita { get; set; } = null!;
        public SignosVitales? SignosVitales { get; set; }
    }
}
