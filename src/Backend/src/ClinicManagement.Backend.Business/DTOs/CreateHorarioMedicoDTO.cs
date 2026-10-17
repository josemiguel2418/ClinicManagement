using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CreateHorarioMedicoDTO
    {
        public int MedicoId { get; set; }
        public int ConsultorioId { get; set; }
        public Dia Dia { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
