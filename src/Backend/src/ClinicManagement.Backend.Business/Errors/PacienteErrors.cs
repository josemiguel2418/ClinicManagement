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
    }
}
