using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface IMedicoService
    {
        public Task<List<MedicoDTO>> GetAllAsync(PaginationDTO pagination);
        public Task<Result<MedicoDTO>> GetByIdAsync(int id);
        public Task<Result<MedicoDTO>> CreateAsync(CreateMedicoDTO modeloDTO);
        public Task<Result> UpdateAsync(int id, UpdateMedicoDTO modeloDTO);
        public Task<Result> DeleteAsync(int id);
        Task<int> CountRecordsAsync();
    }
}
