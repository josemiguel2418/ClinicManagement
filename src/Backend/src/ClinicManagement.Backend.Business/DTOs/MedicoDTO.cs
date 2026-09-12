using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class MedicoDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required TipoIdentificacionDTO TipoIdentificacion { get; set; } 
        public required string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public required string Telefono { get; set; }
        public string? TelefonoEmergencia { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<EspecialidadDTO> MedicoEspecialidades { get; set; } = [];
    }
}
