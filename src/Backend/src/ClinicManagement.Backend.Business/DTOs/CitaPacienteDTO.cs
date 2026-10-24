using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CitaPacienteDTO
    {
        public required string Nombre { get; set; }
        public Sexo? Sexo { get; set; }
        public string Telefono { get; set; } = null!;
        public string? TelefonoEmergencia { get; set; }
    }
}
