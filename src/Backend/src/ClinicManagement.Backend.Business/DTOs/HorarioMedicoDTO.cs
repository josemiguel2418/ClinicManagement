using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class HorarioMedicoDTO
    {
        public int Id { get; set; }
        public Dia Dia { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Activo { get; set; }
        public required MedicoHorarioDTO  Medico {  get; set; }
        public required ConsultorioHorarioDTO Consultorio {  get; set; }
    }
}
