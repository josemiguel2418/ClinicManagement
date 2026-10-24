using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CitaMedicoDTO
    {
        public required string Nombre { get; set; }
        public Sexo Sexo { get; set; }
        public required string Telefono { get; set; }
        public string? TelefonoEmergencia { get; set; }
        public IEnumerable<string> MedicoEspecialidades { get; set; } = [];
    }
}
