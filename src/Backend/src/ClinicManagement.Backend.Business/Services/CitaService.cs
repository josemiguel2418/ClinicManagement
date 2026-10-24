using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class CitaService : ICitaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.CitaRepository
                .CountRecordsAsync();
        }

        public async Task<List<CitaDTO>> GetAllAsync(
            PaginationDTO pagination)
        {
            var citasDB = await _unitOfWork.CitaRepository
                .GetAllAsync(
                    pagination.Pagina,
                    pagination.RecordsPorPagina);

            return _mapper.Map<List<CitaDTO>>(citasDB);
        }

        public async Task<Result<CitaDTO>> GetByIdAsync(int id)
        {
            var citaDB = await _unitOfWork.CitaRepository
                .GetByIdAsync(id);

            if (citaDB is null)
            {
                return Result<CitaDTO>.Failure(
                    CitaErrors.NotFound(id));
            }

            var citaDTO = _mapper.Map<CitaDTO>(citaDB);

            return Result<CitaDTO>.Success(citaDTO);
        }

        public async Task<Result<CitaDTO>> CreateAsync(
            CreateCitaDTO modeloDTO)
        {
            if (modeloDTO.Fecha == default)
            {
                return Result<CitaDTO>.Failure(
                    CitaErrors.InvalidDate);
            }

            if (modeloDTO.HoraInicio >= modeloDTO.HoraFin)
            {
                return Result<CitaDTO>.Failure(
                    CitaErrors.InvalidTimeRange);
            }

            var errorFechaHora = ValidarFechaHoraInicio(modeloDTO.Fecha,modeloDTO.HoraInicio);

            if (errorFechaHora is not null)
            {
                return Result<CitaDTO>.Failure(errorFechaHora);
            }

            var medicoDB = await _unitOfWork.MedicoRepository
               .GetByIdAsync(modeloDTO.MedicoId);

            if (medicoDB is null)
            {
                return Result<CitaDTO>.Failure(CitaErrors.MedicoNotFound(modeloDTO.MedicoId));
            }

            if (!medicoDB.Activo)
            {
                return Result<CitaDTO>.Failure(CitaErrors.MedicoInactive(modeloDTO.MedicoId));
            }

            var pacienteDB = await _unitOfWork.PacienteRepository
                .GetByIdAsync(modeloDTO.PacienteId);

            if (pacienteDB is null)
            {
                return Result<CitaDTO>.Failure(CitaErrors.PacienteNotFound(modeloDTO.PacienteId));
            }

            if (!pacienteDB.Activo)
            {
                return Result<CitaDTO>.Failure(CitaErrors.PacienteInactive(modeloDTO.PacienteId));
            }

            var errorDisponibilidad = await ValidarDisponibilidadAsync(
                modeloDTO.MedicoId,
                modeloDTO.PacienteId,
                modeloDTO.Fecha,
                modeloDTO.HoraInicio,
                modeloDTO.HoraFin);

            if (errorDisponibilidad is not null)
            {
                return Result<CitaDTO>.Failure(errorDisponibilidad);
            }

            var cita = _mapper.Map<Cita>(modeloDTO);

            cita.EstadoCita = EstadoCita.Programada;
            cita.FechaCreacion = DateTime.UtcNow;
            _unitOfWork.CitaRepository.Add(cita);

            await _unitOfWork.Save();

            var citaDTO = _mapper.Map<CitaDTO>(cita);
            citaDTO.Paciente = new CitaPacienteDTO
            {
                Nombre = pacienteDB.Nombre + " " + pacienteDB.Apellido,
                Sexo = pacienteDB.Sexo,
                Telefono = pacienteDB.Telefono,
                TelefonoEmergencia = pacienteDB.TelefonoEmergencia
            };
            citaDTO.Medico = new CitaMedicoDTO
            {
                Nombre = medicoDB.Nombre + " " + medicoDB.Apellido,
                Telefono = medicoDB.Telefono,
                TelefonoEmergencia = medicoDB.TelefonoEmergencia,
                Sexo = medicoDB.Sexo,
                MedicoEspecialidades = medicoDB.MedicoEspecialidades.Select(me => me.Especialidad.Nombre)
            };

            return Result<CitaDTO>.Success(citaDTO);
        }

        public async Task<Result> UpdateAsync(
            int id,
            UpdateCitaDTO modeloDTO)
        {
            var citaDB = await _unitOfWork.CitaRepository
                .GetByIdAsync(id);

            if (citaDB is null)
            {
                return Result.Failure(CitaErrors.NotFound(id));
            }

            if (citaDB.EstadoCita is
                EstadoCita.Cancelada or
                EstadoCita.Completada or
                EstadoCita.NoAsistio)
            {
                return Result.Failure(
                    CitaErrors.CitaNotModifiable(id));
            }

            if (modeloDTO.Fecha == default)
            {
                return Result.Failure(
                    CitaErrors.InvalidDate);
            }

            if (modeloDTO.HoraInicio >= modeloDTO.HoraFin)
            {
                return Result.Failure(
                    CitaErrors.InvalidTimeRange);
            }

            var errorFechaHora = ValidarFechaHoraInicio(modeloDTO.Fecha,modeloDTO.HoraInicio);

            if (errorFechaHora is not null)
            {
                return Result<CitaDTO>.Failure(errorFechaHora);
            }

            var medicoDB = await _unitOfWork.MedicoRepository
               .GetByIdAsync(modeloDTO.MedicoId);

            if (medicoDB is null)
            {
                return Result<CitaDTO>.Failure(CitaErrors.MedicoNotFound(modeloDTO.MedicoId));
            }

            if (!medicoDB.Activo)
            {
                return Result<CitaDTO>.Failure(CitaErrors.MedicoInactive(modeloDTO.MedicoId));
            }

            var pacienteDB = await _unitOfWork.PacienteRepository
                .GetByIdAsync(modeloDTO.PacienteId);

            if (pacienteDB is null)
            {
                return Result<CitaDTO>.Failure(CitaErrors.PacienteNotFound(modeloDTO.PacienteId));
            }

            if (!pacienteDB.Activo)
            {
                return Result<CitaDTO>.Failure(CitaErrors.PacienteInactive(modeloDTO.PacienteId));
            }

            var errorDisponibilidad = await ValidarDisponibilidadAsync(
                modeloDTO.MedicoId,
                modeloDTO.PacienteId,
                modeloDTO.Fecha,
                modeloDTO.HoraInicio,
                modeloDTO.HoraFin,
                id);

            if (errorDisponibilidad is not null)
            {
                return Result.Failure(errorDisponibilidad);
            }

            _mapper.Map(modeloDTO, citaDB); ;

            await _unitOfWork.Save();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var citaDB = await _unitOfWork.CitaRepository
                .GetByIdAsync(id);

            if (citaDB is null)
            {
                return Result.Failure(CitaErrors.NotFound(id));
            }

            if (citaDB.EstadoCita == EstadoCita.Cancelada)
            {
                return Result.Success();
            }

            if (citaDB.EstadoCita is
                EstadoCita.Completada or
                EstadoCita.NoAsistio)
            {
                return Result.Failure(
                    CitaErrors.CitaNotCancellable(id));
            }

            citaDB.EstadoCita = EstadoCita.Cancelada;

            await _unitOfWork.Save();

            return Result.Success();
        }

        private async Task<Error?> ValidarDisponibilidadAsync(
            int medicoId,
            int pacienteId,
            DateOnly fecha,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            int? excludedId = null)
        {
            var dia = ObtenerDiaSemana(fecha);

            var tieneHorario = await _unitOfWork
                .HorarioMedicoRepository
                .HasActiveScheduleCoveringAsync(
                    medicoId,
                    dia,
                    horaInicio,
                    horaFin);

            if (!tieneHorario)
            {
                return CitaErrors.ScheduleUnavailable;
            }

            var tieneConflicto = await _unitOfWork
                .CitaRepository
                .HasConflictAsync(
                    medicoId,
                    pacienteId,
                    fecha,
                    horaInicio,
                    horaFin,
                    excludedId);

            if (tieneConflicto)
            {
                return CitaErrors.ScheduleConflict;
            }

            return null;
        }

        private static Error? ValidarFechaHoraInicio(DateOnly fecha, TimeOnly horaInicio)
        {
            var ahora = DateTime.Now;

            var fechaActual = DateOnly.FromDateTime(ahora);
            var horaActual = TimeOnly.FromDateTime(ahora);

            if (fecha < fechaActual)
            {
                return CitaErrors.DateInThePast;
            }

            if (fecha == fechaActual && horaInicio < horaActual)
            {
                return CitaErrors.StartTimeInThePast;
            }

            return null;
        }

        private static Dia ObtenerDiaSemana(DateOnly fecha)
        {
            return fecha.DayOfWeek switch
            {
                DayOfWeek.Monday => Dia.Lunes,
                DayOfWeek.Tuesday => Dia.Martes,
                DayOfWeek.Wednesday => Dia.Miercoles,
                DayOfWeek.Thursday => Dia.Jueves,
                DayOfWeek.Friday => Dia.Viernes,
                DayOfWeek.Saturday => Dia.Sabado,
                DayOfWeek.Sunday => Dia.Domingo,

                _ => throw new ArgumentOutOfRangeException(
                    nameof(fecha))
            };
        }
    }
}

