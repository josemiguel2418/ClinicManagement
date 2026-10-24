namespace ClinicManagement.Backend.Business.Errors
{
    public static class CitaErrors
    {
        public static readonly Error InvalidDate = new(
                "Cita.FechaInvalida",
                "La fecha de la cita no es válida.",
                ErrorType.Conflict);


        public static readonly Error InvalidTimeRange = new(
                "Cita.HorasInvalidas",
                "La hora de inicio debe ser anterior a la hora de fin.",
                ErrorType.Conflict);
        public static readonly Error ScheduleUnavailable = new(
                "Cita.HorarioNoDisponible",
                "El intervalo solicitado no está dentro del horario laboral activo del médico.",
                ErrorType.Conflict);


        public static readonly Error ScheduleConflict = new(
                "Cita.ConflictoHorario",
                "El médico o el paciente ya tiene una cita que se superpone con ese intervalo.",
                ErrorType.Conflict);

        public static readonly Error DateInThePast = new(
                "Cita.FechaAnteriorAActual",
                "La fecha de la cita no puede ser anterior a la fecha actual.",
                ErrorType.Conflict);

        public static readonly Error StartTimeInThePast = new(
                "Cita.HoraInicioAnteriorAActual",
                "La hora de inicio de la cita no puede ser anterior al momento actual.",
                ErrorType.Conflict);

        public static Error NotFound(int id)
        {
            return new Error(
               "Cita.NotFound",
               $"No existe una cita con el identificador {id}.",
               ErrorType.NotFound);
        }


        public static Error MedicoNotFound(int id)
        {
            return new Error(
                "Cita.MedicoNotFound",
                $"No existe un médico con el identificador {id}.",
                ErrorType.NotFound);
        }


        public static Error MedicoInactive(int id)
        {
            return new Error(
                "Cita.MedicoInactivo",
                $"El médico con identificador {id} está inactivo.",
                ErrorType.Conflict);
        }

        public static Error PacienteNotFound(int id)
        {
            return new Error(
                "Cita.PacienteNotFound",
                $"No existe un paciente con el identificador {id}.",
                ErrorType.NotFound);
        }

        public static Error PacienteInactive(int id)
        {
            return new Error(
                "Cita.PacienteInactivo",
                $"El paciente con identificador {id} está inactivo.",
                ErrorType.Conflict);
        }
        public static Error CitaNotModifiable(int id)
        {
            return new Error(
                "Cita.NoModificable",
                $"La cita {id} no puede modificarse debido a su estado actual.",
                ErrorType.Conflict);
        }


        public static Error CitaNotCancellable(int id)
        {
            return new Error(
                "Cita.NoCancelable",
                $"La cita {id} ya fue completada o marcada como no asistida.",
                ErrorType.Conflict);
        }

    }
}
