using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface IPacienteService
    {
        public Task<List<PacienteDTO>> GetAllAsync(PaginationDTO pagination);
        public Task<Result<PacienteDTO>> GetByIdAsync(int id);
        public Task<Result<PacienteDTO>> CreateAsync(CreatePacienteDTO modeloDTO);
        public Task<Result> UpdateAsync(int id, UpdatePacienteDTO modeloDTO);
        public Task<Result> DeleteAsync(int id);
        Task<int> CountRecordsAsync();
    }
}
