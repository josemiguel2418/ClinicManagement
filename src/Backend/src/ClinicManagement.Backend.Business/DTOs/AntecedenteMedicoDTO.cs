namespace ClinicManagement.Backend.Business.DTOs
{
    public class AntecedenteMedicoDTO
    {
        public int Id { get; set; }
        public int TipoAntecedenteMedicoId { get; set; }
        public required string Descripcion { get; set; }
    }
}
