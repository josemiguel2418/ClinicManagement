using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;

        public DateOnly Fecha { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string? Motivo { get; set; }

        public EstadoCita EstadoCita { get; set; }
    }
}
