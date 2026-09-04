namespace ClinicManagement.Backend.Domain.Entities
{
    public class AntecedenteMedico
    {
        public int Id { get; set; }
        public TipoAntecedenteMedico TipoAntecedenteMedico { get; set; } = null!;
        public int TipoAntecedenteMedicoId { get; set; }
        public string Descripcion { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
        public int PacienteId { get; set; }
           
    }
}
