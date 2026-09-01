using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using MapsterMapper;

namespace ClinicManagement.Backend.Business.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsultorioService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<List<ConsultorioDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var consultoriosDB = await _unitOfWork.ConsultorioRepository.GetAllAsync(pagination.Pagina,pagination.RecordsPorPagina);
            var consultoriosDTO = _mapper.Map<List<ConsultorioDTO>>(consultoriosDB);
            return consultoriosDTO;
        }

        public async Task<Result<ConsultorioDTO>> GetByIdAsync(int id)
        {
            var consultorioDb = await _unitOfWork.ConsultorioRepository.GetByIdAsync(id);
            if (consultorioDb is null)
            {
                return Result<ConsultorioDTO>.Failure(ConsultorioErrors.NotFound(id));
            }
            var dto = _mapper.Map<ConsultorioDTO>(consultorioDb);
            return Result<ConsultorioDTO>.Success(dto);
        }

        public async Task<Result<ConsultorioDTO>> CreateAsync(CreateConsultorioDTO modeloDTO)
        {
            var consultorioAlreadyExists = await _unitOfWork.ConsultorioRepository.GetByNameAsync(modeloDTO.Nombre);
            if (consultorioAlreadyExists is not null)
            {
                return Result<ConsultorioDTO>.Failure(ConsultorioErrors.NameAlreadyExists);
            }
            var consultorio = _mapper.Map<Consultorio>(modeloDTO);
            consultorio.Activo = true;
            _unitOfWork.ConsultorioRepository.Add(consultorio);
            await _unitOfWork.Save();
            var dto = _mapper.Map<ConsultorioDTO>(consultorio);
            return Result<ConsultorioDTO>.Success(dto);
        }

        public async Task<Result> UpdateAsync(int id, UpdateConsultorioDTO modeloDTO)
        {
            var consultorioDb = await _unitOfWork.ConsultorioRepository.GetByIdAsync(id);
            if (consultorioDb is null)
            {
                return Result.Failure(ConsultorioErrors.NotFound(id));
            }

            var consultorioAlreadyExists = await _unitOfWork.ConsultorioRepository.GetByNameAsync(modeloDTO.Nombre);
            if(consultorioAlreadyExists is not null && consultorioAlreadyExists.Id != id)
            {
                return Result.Failure(ConsultorioErrors.NameAlreadyExists);

            }

            _mapper.Map(modeloDTO, consultorioDb);
            
            await _unitOfWork.Save();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var consultorioDB = await _unitOfWork.ConsultorioRepository.GetByIdAsync(id);
            if (consultorioDB is null)
            {
                return Result.Failure(ConsultorioErrors.NotFound(id));

            }
            consultorioDB.Activo = false;
            await _unitOfWork.Save();
            return Result.Success();

        }
        public async Task<int> CountRecordsAsync()
        {
            return await _unitOfWork.ConsultorioRepository.CountRecordsAsync();
        }
    }
}
