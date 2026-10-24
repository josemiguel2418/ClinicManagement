using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class UpdateCitaDTO
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string? Motivo { get; set; }
    }
}
