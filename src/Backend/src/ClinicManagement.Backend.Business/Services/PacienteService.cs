using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PacienteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.PacienteRepository.CountRecordsAsync();
        }
        public async Task<List<PacienteDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var pacientesDB = await _unitOfWork.PacienteRepository.GetAllAsync(pagination.Pagina, pagination.RecordsPorPagina);
            var pacientesDTO = _mapper.Map<List<PacienteDTO>>(pacientesDB);
            return pacientesDTO;
        }

        public async Task<Result<PacienteDTO>> GetByIdAsync(int id)
        {
            var pacienteDB = await _unitOfWork.PacienteRepository.GetByIdAsync(id);
            if (pacienteDB is null)
            {
                return Result<PacienteDTO>.Failure(PacienteErrors.NotFound(id));
            }
            var pacienteDTO = _mapper.Map<PacienteDTO>(pacienteDB);
            return Result<PacienteDTO>.Success(pacienteDTO);
        }

        public async Task<Result<PacienteDTO>> CreateAsync(CreatePacienteDTO modeloDTO)
        {
            var pacienteAlreadyExists = await _unitOfWork.PacienteRepository.GetByIdentificacionAsync(modeloDTO.Identificacion, modeloDTO.TipoIdentificacionId);
            if (pacienteAlreadyExists is not null)
            {
                return Result<PacienteDTO>.Failure(PacienteErrors.AlreadyExists(modeloDTO.Identificacion));
            }

            var antecedentesDTO = modeloDTO.AntecedentesMedicos ?? [];

            var tipoInvalido = await ObtenerTipoAntecedenteInvalidoAsync(
                antecedentesDTO.Select(a => a.TipoAntecedenteMedicoId));

            if (tipoInvalido.HasValue)
            {
                return Result<PacienteDTO>.Failure(
                    PacienteErrors.TipoNoEncontrado(tipoInvalido.Value));
            }

            var paciente = _mapper.Map<Paciente>(modeloDTO);
            paciente.AntecedentesMedicos = antecedentesDTO.Select(a => new AntecedenteMedico
            {
                TipoAntecedenteMedicoId = a.TipoAntecedenteMedicoId,
                Descripcion = a.Descripcion.Trim()
            }).ToList();
            paciente.Activo = true;
            paciente.FechaCreacion = DateTime.UtcNow;
            _unitOfWork.PacienteRepository.Add(paciente);
            await _unitOfWork.Save();
            var pacienteDTO = _mapper.Map<PacienteDTO>(paciente);
            return Result<PacienteDTO>.Success(pacienteDTO);
        }

        public async Task<Result> UpdateAsync(int id, UpdatePacienteDTO modeloDTO)
        {
            var pacienteDB = await _unitOfWork.PacienteRepository.GetByIdAsync(id);
            if (pacienteDB is null)
            {
                return Result.Failure(PacienteErrors.NotFound(id));
            }

            var pacienteAlreadyExists = await _unitOfWork.PacienteRepository.GetByIdentificacionAsync(modeloDTO.Identificacion, modeloDTO.TipoIdentificacionId);
            if (pacienteAlreadyExists is not null && pacienteAlreadyExists.Id != id)
            {
                return Result.Failure(PacienteErrors.AlreadyExists(pacienteAlreadyExists.Identificacion));

            }

            var antecedentesDTO = modeloDTO.AntecedentesMedicos;
            var antecedentesActuales = pacienteDB.AntecedentesMedicos.ToList();
            List<int> idsRecibidos = [];

            if (antecedentesDTO is not null)
            {
                var tipoInvalido = await ObtenerTipoAntecedenteInvalidoAsync(
                    antecedentesDTO.Select(a => a.TipoAntecedenteMedicoId));

                if (tipoInvalido.HasValue)
                {
                    return Result.Failure(
                        PacienteErrors.TipoNoEncontrado(
                            tipoInvalido.Value));
                }

                idsRecibidos = antecedentesDTO
                    .Where(a => a.Id.HasValue)
                    .Select(a => a.Id!.Value)
                    .ToList();

                if (idsRecibidos.Count != idsRecibidos.Distinct().Count())
                {
                    return Result.Failure(
                        PacienteErrors.IdsDuplicados());
                }

                int? idNoPertenece = idsRecibidos
                    .Where(antecedenteId =>
                        antecedentesActuales.All(a => a.Id != antecedenteId))
                    .Select(antecedenteId => (int?)antecedenteId)
                    .FirstOrDefault();

                if (idNoPertenece.HasValue)
                {
                    return Result.Failure(
                        PacienteErrors.NoPerteneceAlPaciente(idNoPertenece.Value));
                }
            }

            _mapper.Map(modeloDTO, pacienteDB);

            if (antecedentesDTO is not null)
            {
                foreach (var antecedenteDTO in antecedentesDTO)
                {
                    if (antecedenteDTO.Id is int antecedenteId)
                    {
                        var antecedenteExistente = antecedentesActuales
                            .Single(a => a.Id == antecedenteId);

                        antecedenteExistente.TipoAntecedenteMedicoId =
                            antecedenteDTO.TipoAntecedenteMedicoId;

                        antecedenteExistente.Descripcion =
                            antecedenteDTO.Descripcion.Trim();
                    }
                    else
                    {
                        pacienteDB.AntecedentesMedicos.Add(
                            new AntecedenteMedico
                            {
                                TipoAntecedenteMedicoId =
                                    antecedenteDTO.TipoAntecedenteMedicoId,

                                Descripcion = antecedenteDTO.Descripcion.Trim()
                            });
                    }
                }

                var antecedentesAEliminar = antecedentesActuales.Where(a => !idsRecibidos.Contains(a.Id)).ToList();

                foreach (var antecedente in antecedentesAEliminar)
                {
                    _unitOfWork.AntecedenteMedicoRepository.Remove(antecedente);
                }
            }
           
            await _unitOfWork.Save();
            return Result.Success();

        }

        public async Task<Result> DeleteAsync(int id)
        {
            var pacienteDB = await _unitOfWork.PacienteRepository.GetByIdAsync(id);
            if (pacienteDB is null)
            {
                return Result.Failure(PacienteErrors.NotFound(id));
            }
            pacienteDB.Activo = false;
            await _unitOfWork.Save();
            return Result.Success();
        }

        private async Task<int?> ObtenerTipoAntecedenteInvalidoAsync(IEnumerable<int> tipoIds)
        {
            foreach (var tipoId in tipoIds.Distinct())
            {
                var tipo = await _unitOfWork.TipoAntecedenteMedicoRepository.GetByIdAsync(tipoId);

                if (tipo is null)
                {
                    return tipoId;
                }
            }

            return null;
        }

    }
}
