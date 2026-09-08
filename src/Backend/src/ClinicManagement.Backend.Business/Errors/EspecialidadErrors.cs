namespace ClinicManagement.Backend.Business.Errors
{
    public static class EspecialidadErrors
    {
        public static readonly Error NameAlreadyExists = new("Especialidad.NombreYaExiste", "Ya existe una especialidad con ese nombre.", ErrorType.Conflict);
        public static Error NotFound(int id)
        {
            return new Error("Especialidad.NoEncontrado", $"La Especialidad con id {id} no ha sido encontrada.", ErrorType.NotFound);
        }
    }
}
