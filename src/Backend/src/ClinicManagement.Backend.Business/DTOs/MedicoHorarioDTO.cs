using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class MedicoHorarioDTO
    {
        public required string Nombre { get; set; }
        public Sexo Sexo { get; set; }
        public IEnumerable<string> Especialidades { get; set; } = [];
    }
}
