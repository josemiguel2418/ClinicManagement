namespace ClinicManagement.Backend.Business.Errors
{
    public static class PacienteErrors
    {
        public static Error AlreadyExists(string identificacion)
        {
            return new Error("Paciente.YaExiste", $"El paciente con la identificación {identificacion} ya existe.", ErrorType.Conflict);
        }
        public static Error NotFound(int id)
        {
            return new Error("Paciente.NoEncontrado", $"El paciente con id {id} no ha sido encontrado.", ErrorType.NotFound);
        }

        public static Error TipoNoEncontrado(int id)
        {
            return new Error(
           "Paciente.TipoAntecedenteMedicoNoEncontrado",
           $"No existe el tipo de antecedente médico con Id {id}.",
           ErrorType.NotFound);
        }

        public static Error NoPerteneceAlPaciente(int id)
        {
            return new Error(
                "Paciente.TipoAntecedenteMedicoNoPerteneceAlPaciente",
                $"El antecedente médico con Id {id} no pertenece al paciente.",
                ErrorType.NotFound);
        }
        public static Error IdsDuplicados()
        {
            return new Error(
               "Paciente.IdsAntecedentesMedicosDuplicados",
               "La solicitud contiene antecedentes médicos duplicados.",
               ErrorType.Conflict);
        }


    }
}
