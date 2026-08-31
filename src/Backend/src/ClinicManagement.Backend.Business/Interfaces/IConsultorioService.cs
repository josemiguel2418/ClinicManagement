using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface IConsultorioService
    {
        public Task<List<ConsultorioDTO>> GetAllAsync(PaginationDTO pagination);
        public Task<Result<ConsultorioDTO>> GetByIdAsync(int id);
        public Task<Result<ConsultorioDTO>> CreateAsync(CreateConsultorioDTO modeloDTO);
        public Task<Result> UpdateAsync(int id, UpdateConsultorioDTO modeloDTO);
        public Task<Result> DeleteAsync(int id);
        Task<int> CountRecordsAsync();



    }
}
