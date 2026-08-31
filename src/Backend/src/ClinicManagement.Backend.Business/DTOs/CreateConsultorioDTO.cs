using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CreateConsultorioDTO 
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [MaxLength(100,ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public byte Piso { get; set; }
    }
}
