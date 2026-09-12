using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<int> CountRecordsAsync()
        {
            return _unitOfWork.MedicoRepository.CountRecordsAsync();
        }

        public async Task<Result<MedicoDTO>> CreateAsync(CreateMedicoDTO modeloDTO)
        {
            var especialidadesIds = modeloDTO.EspecialidadesIds.Distinct();
            var especialidadesIdsDB =await _unitOfWork.EspecialidadRepository.GetExistingIdsAsync(especialidadesIds);

            if (especialidadesIds.Count() != especialidadesIdsDB.Count()) 
            {
                var invalidIds = especialidadesIds.Except(especialidadesIdsDB);
                return Result<MedicoDTO>.Failure(MedicoErrors.InvalidEspecialidadIds(invalidIds));
            }

            var medicoAlreadyExists = await _unitOfWork.MedicoRepository.GetByIdentificacionAsync(modeloDTO.Identificacion, modeloDTO.TipoIdentificacionId);

            if(medicoAlreadyExists is not null)
            {
                return Result<MedicoDTO>.Failure(MedicoErrors.AlreadyExists(modeloDTO.Identificacion));
            }

            var medico = _mapper.Map<Medico>(modeloDTO);
            medico.Activo = true;
            medico.FechaCreacion = DateTime.UtcNow;
            _unitOfWork.MedicoRepository.Add(medico);
            await _unitOfWork.Save();

            var medicoDTO = _mapper.Map<MedicoDTO>(medico);
            return Result<MedicoDTO>.Success(medicoDTO);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var medicoDB = await _unitOfWork.MedicoRepository.GetByIdAsync(id);
            if (medicoDB is null)
            {
                return Result.Failure(PacienteErrors.NotFound(id));
            }
            medicoDB.Activo = false;
            await _unitOfWork.Save();
            return Result.Success();
        }

        public async Task<List<MedicoDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var medicosDB = await _unitOfWork.MedicoRepository.GetAllAsync(pagination.Pagina, pagination.RecordsPorPagina);
            var medicosDTO = _mapper.Map<List<MedicoDTO>>(medicosDB);
            return medicosDTO;
        }

        public async Task<Result<MedicoDTO>> GetByIdAsync(int id)
        {
            var medicoDB = await _unitOfWork.MedicoRepository.GetByIdAsync(id);
            if (medicoDB is null)
            {
                return Result<MedicoDTO>.Failure(MedicoErrors.NotFound(id));
            }
            var medicoDTO = _mapper.Map<MedicoDTO>(medicoDB);
            return Result<MedicoDTO>.Success(medicoDTO);
        }

        public async Task<Result> UpdateAsync(int id, UpdateMedicoDTO modeloDTO)
        {
            var medicoDB = await _unitOfWork.MedicoRepository.GetByIdAsync(id);
            if (medicoDB is null)
            {
                return Result.Failure(MedicoErrors.NotFound(id));
            }

            var medicoAlreadyExists = await _unitOfWork.MedicoRepository.GetByIdentificacionAsync(modeloDTO.Identificacion, modeloDTO.TipoIdentificacionId);
            if (medicoAlreadyExists is not null && medicoAlreadyExists.Id != id)
            {
                return Result.Failure(MedicoErrors.AlreadyExists(medicoAlreadyExists.Identificacion));

            }

            var especialidadesIds = modeloDTO.EspecialidadesIds.Distinct();
            var especialidadesIdsDB = await _unitOfWork.EspecialidadRepository.GetExistingIdsAsync(especialidadesIds);

            if (especialidadesIds.Count() != especialidadesIdsDB.Count())
            {
                var invalidIds = especialidadesIds.Except(especialidadesIdsDB);
                return Result<MedicoDTO>.Failure(MedicoErrors.InvalidEspecialidadIds(invalidIds));
            }

            _mapper.Map(modeloDTO, medicoDB);
            await _unitOfWork.Save();
            return Result.Success();
        }
    }
}
