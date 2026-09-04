using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Domain.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public Sexo? Sexo { get; set; }
        public TipoSangre? TipoSangre { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; } = null!;
        public int TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? TelefonoEmergencia { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<AntecedenteMedico> AntecedentesMedicos { get; set; } = [];
    }
}
