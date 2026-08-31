using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Results;
using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;

namespace ClinicManagement.Backend.Business.Services
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultorioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<ConsultorioDTO>> GetAllAsync(PaginationDTO pagination)
        {
            var consultoriosDB = await _unitOfWork.ConsultorioRepository.GetAllAsync(pagination.Pagina,pagination.RecordsPorPagina);
            var consultoriosDTO = consultoriosDB.Select(consultorio =>
            {
                return new ConsultorioDTO
                {
                    Id = consultorio.Id,
                    Nombre = consultorio.Nombre,
                    Activo = consultorio.Activo,
                    Piso = consultorio.Piso
                };
            }).ToList();
            return consultoriosDTO;
        }

        public async Task<Result<ConsultorioDTO>> GetByIdAsync(int id)
        {
            var consultorioDb = await _unitOfWork.ConsultorioRepository.GetByIdAsync(id);
            if (consultorioDb is null)
            {
                return Result<ConsultorioDTO>.Failure(ConsultorioErrors.NotFound(id));
            }
            var dto = new ConsultorioDTO
            {
                Id = consultorioDb.Id,
                Nombre = consultorioDb.Nombre,
                Activo = consultorioDb.Activo,
                Piso = consultorioDb.Piso
            };
            return Result<ConsultorioDTO>.Success(dto);
        }

        public async Task<Result<ConsultorioDTO>> CreateAsync(CreateConsultorioDTO modeloDTO)
        {
            var consultorioAlreadyExists = await _unitOfWork.ConsultorioRepository.GetByNameAsync(modeloDTO.Nombre);
            if (consultorioAlreadyExists is not null)
            {
                return Result<ConsultorioDTO>.Failure(ConsultorioErrors.NameAlreadyExists);
            }
            var consultorio = new Consultorio
            {
                Nombre = modeloDTO.Nombre,
                Piso = modeloDTO.Piso,
                Activo = true
            };
            _unitOfWork.ConsultorioRepository.Add(consultorio);
            await _unitOfWork.Save();
            var dto = new ConsultorioDTO
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre,
                Activo = consultorio.Activo,
                Piso = consultorio.Piso
            };
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

            consultorioDb.Nombre = modeloDTO.Nombre;
            consultorioDb.Piso = modeloDTO.Piso;
            
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
