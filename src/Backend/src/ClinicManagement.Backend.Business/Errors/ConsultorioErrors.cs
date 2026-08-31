namespace ClinicManagement.Backend.Business.Errors
{
    public static class ConsultorioErrors
    {
        
        public static readonly Error NameAlreadyExists = new("Consultorio.NombreYaExiste", "Ya existe un consultorio con ese nombre.",ErrorType.Conflict);

        public static Error NotFound(int id)
        {
            return new Error("Consultorio.NoEncontrado", $"El consultorio con id {id} no ha sido encontrado.", ErrorType.NotFound);
        }
    }
}
