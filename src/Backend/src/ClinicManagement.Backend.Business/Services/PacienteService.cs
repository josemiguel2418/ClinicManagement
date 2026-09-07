using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    internal class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PacienteService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.PacienteRepository.CountRecordsAsync();
        }

        public async Task<Result<PacienteDTO>> CreateAsync(CreatePacienteDTO modeloDTO)
        {
            var pacienteAlreadyExists = await _unitOfWork.PacienteRepository.GetByIdentificacionAsync(modeloDTO.Identificacion, modeloDTO.TipoIdentificacionId);
            if(pacienteAlreadyExists is not null)
            {
                return Result<PacienteDTO>.Failure(PacienteErrors.AlreadyExists(modeloDTO.Identificacion));
            }
            var paciente = _mapper.Map<Paciente>(modeloDTO);
            paciente.Activo = true;
            paciente.FechaCreacion = DateTime.UtcNow;
            _unitOfWork.PacienteRepository.Add(paciente);
            await _unitOfWork.Save();
            var pacienteDTO = _mapper.Map<PacienteDTO>(paciente);
            return Result<PacienteDTO>.Success(pacienteDTO);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var pacienteDB = await _unitOfWork.PacienteRepository.GetByIdAsync(id);
            if(pacienteDB is null)
            {
                return Result.Failure(PacienteErrors.NotFound(id));
            }
            pacienteDB.Activo = false;
            await _unitOfWork.Save();
            return Result.Success();
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

        public async Task<Result> UpdateAsync(int id, UpdatePacienteDTO modeloDTO)
        {
            var pacienteDB = await _unitOfWork.PacienteRepository.GetByIdAsync(id);
            if (pacienteDB is null)
            {
                return Result<PacienteDTO>.Failure(PacienteErrors.NotFound(id));
            }

            var pacienteAlreadyExists = await _unitOfWork.PacienteRepository.GetByIdentificacionAsync(modeloDTO.Identificacion,modeloDTO.TipoIdentificacionId);
            if (pacienteAlreadyExists is not null && pacienteAlreadyExists.Id != id)
            {
                return Result.Failure(PacienteErrors.AlreadyExists(pacienteAlreadyExists.Identificacion));

            }

            _mapper.Map(modeloDTO,pacienteDB);
            await _unitOfWork.Save();
            return Result.Success();
        }
    }
}
