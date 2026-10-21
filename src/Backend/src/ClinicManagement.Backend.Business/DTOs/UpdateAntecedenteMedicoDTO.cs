using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class UpdateAntecedenteMedicoDTO
    {
        public int? Id { get; set; }
        public int TipoAntecedenteMedicoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Descripcion { get; set; }
    }
}
