using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EspecialidadService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.EspecialidadRepository.CountRecordsAsync();
        }

        public async Task<Result<EspecialidadDTO>> CreateAsync(CreateEspecialidadDTO modeloDTO)
        {
            var especialidadDB = await _unitOfWork.EspecialidadRepository.GetByNombreAsync(modeloDTO.Nombre);
            if(especialidadDB is not null)
            {
                return Result<EspecialidadDTO>.Failure(EspecialidadErrors.NameAlreadyExists);
            }
            var especialidad = _mapper.Map<Especialidad>(modeloDTO);
            _unitOfWork.EspecialidadRepository.Add(especialidad);
            await _unitOfWork.Save();
            var dto = _mapper.Map<EspecialidadDTO>(especialidad);
            return Result<EspecialidadDTO>.Success(dto);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var especialidadDB = await _unitOfWork.EspecialidadRepository.GetByIdAsync(id);
            if(especialidadDB is null)
            {
                return Result.Failure(EspecialidadErrors.NotFound(id));
            }
            _unitOfWork.EspecialidadRepository.Delete(especialidadDB);
            await _unitOfWork.Save();
            return Result.Success();
        }

        public async Task<List<EspecialidadDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var especialidades = await _unitOfWork.EspecialidadRepository.GetAllAsync(pagination.Pagina, pagination.RecordsPorPagina);
            return _mapper.Map<List<EspecialidadDTO>>(especialidades);
        }

        public async Task<Result<EspecialidadDTO>> GetByIdAsync(int id)
        {
            var especialidadDB = await _unitOfWork.EspecialidadRepository.GetByIdAsync(id);
            if (especialidadDB is null)
            {
                return Result<EspecialidadDTO>.Failure(EspecialidadErrors.NotFound(id));
            }
            var dto = _mapper.Map<EspecialidadDTO>(especialidadDB);
            return Result<EspecialidadDTO>.Success(dto);
        }

        public async Task<Result> UpdateAsync(int id, UpdateEspecialidadDTO modeloDTO)
        {
            var especialidadDB = await _unitOfWork.EspecialidadRepository.GetByIdAsync(id);
            if (especialidadDB is null)
            {
                return Result.Failure(EspecialidadErrors.NotFound(id));
            }
            var especialidadAlreadyExists = await _unitOfWork.ConsultorioRepository.GetByNameAsync(modeloDTO.Nombre);
            if (especialidadAlreadyExists is not null && especialidadAlreadyExists.Id != id)
            {
                return Result.Failure(ConsultorioErrors.NameAlreadyExists);

            }
            _mapper.Map(modeloDTO, especialidadDB);

            await _unitOfWork.Save();
            return Result.Success();
        }
    }
}
