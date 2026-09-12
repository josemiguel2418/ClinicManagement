using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Domain.Entities
{
    public class HorarioMedico
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;
        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; } = null!;
        public Dia Dia { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Activo {  get; set; }

    }
}
