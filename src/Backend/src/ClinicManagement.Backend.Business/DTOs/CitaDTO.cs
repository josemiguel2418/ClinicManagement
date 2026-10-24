using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CitaDTO
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }

        public string? Motivo { get; set; }

        public EstadoCita EstadoCita { get; set; }
        public required CitaMedicoDTO Medico { get; set; }
        public required CitaPacienteDTO Paciente { get; set; }

    }
}
