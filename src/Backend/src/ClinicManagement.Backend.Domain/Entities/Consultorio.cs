namespace ClinicManagement.Backend.Domain.Entities
{
    public class Consultorio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public byte Piso {  get; set; }
        public bool Activo { get; set; }
    }
}
