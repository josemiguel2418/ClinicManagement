namespace ClinicManagement.Backend.Business.Errors
{
    public static class HorarioMedicoErrors
    {
        public static readonly Error ScheduleConflict = new("HorarioMedico.Choque", "El horario se superpone con otro horario existente", ErrorType.Conflict);
        public static readonly Error InvalidSchedule = new("HorarioMedico.HorarioInvalido", "La hora de inicio no puede ser mayor que la de fin", ErrorType.Forbidden);
        public static Error NotFound(int id)
        {
            return new Error("HorarioMedico.NoEncontrado", $"El horario médico con id {id} no ha sido encontrado.", ErrorType.NotFound);
        }
        public static Error MedicoNotFound(int id)
        {
            return new Error(
                "HorarioMedico.MedicoNoEncontrado",
                $"El médico con id {id} no ha sido encontrado.",
                ErrorType.NotFound);
        }

        public static readonly Error ScheduleInactive = new(
    "HorarioMedico.Inactivo",
    "No se puede modificar un horario médico inactivo.",
    ErrorType.Conflict);

        public static Error ConsultorioNotFound(int id)
        {
            return new Error(
                "HorarioMedico.ConsultorioNoEncontrado",
                $"El consultorio con id {id} no ha sido encontrado.",
                ErrorType.NotFound);
        }
        public static readonly Error MedicoInactive = new(
    "HorarioMedico.MedicoInactivo",
    "No se puede asignar un horario a un médico inactivo.",
    ErrorType.Conflict);

        public static readonly Error ConsultorioInactive = new(
            "HorarioMedico.ConsultorioInactivo",
            "No se puede asignar un horario a un consultorio inactivo.",
            ErrorType.Conflict);
    }
}
