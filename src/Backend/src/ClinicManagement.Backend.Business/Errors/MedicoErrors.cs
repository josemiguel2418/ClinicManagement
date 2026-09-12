namespace ClinicManagement.Backend.Business.Errors
{
    public static class MedicoErrors
    {
        public static Error NotFound(int id)
        {
            return new Error("Médico.NoEncontrado", $"El médico con id {id} no ha sido encontrado.", ErrorType.NotFound);
        }

        public static Error AlreadyExists(string identificacion)
        {
            return new Error("Médico.YaExiste", $"El médico con la identificación {identificacion} ya existe.", ErrorType.Conflict);
        }
        public static Error InvalidEspecialidadIds(IEnumerable<int> especialidadIds)
        {
            return new Error("Médico.EspecialidadInvalida", $"Los siguientes ids de especialidades no existen: {string.Join(",", especialidadIds)}.", ErrorType.NotFound);
        }
    }
}
