using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface IEspecialidadService
    {
        public Task<List<EspecialidadDTO>> GetAllAsync(PaginationDTO pagination);
        public Task<Result<EspecialidadDTO>> GetByIdAsync(int id);
        public Task<Result<EspecialidadDTO>> CreateAsync(CreateEspecialidadDTO modeloDTO);
        public Task<Result> UpdateAsync(int id, UpdateEspecialidadDTO modeloDTO);
        public Task<Result> DeleteAsync(int id);
        Task<int> CountRecordsAsync();
    }
}
