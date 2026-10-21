using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicManagement.Backend.Business.DTOs
{
    public class CreateAntecedenteMedicoDTO
    {
        public int TipoAntecedenteMedicoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public required string Descripcion { get; set; }
    }
}
