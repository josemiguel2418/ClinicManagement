using ClinicManagement.Backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CreateMedicoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public required string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public int TipoIdentificacionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public required string Identificacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public required string Telefono { get; set; }
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string? TelefonoEmergencia { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public IEnumerable<int> EspecialidadesIds { get; set; } = [];
    }
}
