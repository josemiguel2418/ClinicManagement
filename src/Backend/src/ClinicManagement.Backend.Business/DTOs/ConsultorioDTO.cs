namespace ClinicManagement.Backend.Business.DTOs
{
    public class ConsultorioDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public byte Piso { get; set; }
        public bool Activo { get; set; }
    }
}
