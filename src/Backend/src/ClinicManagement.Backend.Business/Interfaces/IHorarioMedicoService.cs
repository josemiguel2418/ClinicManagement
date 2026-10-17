using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface IHorarioMedicoService
    {
        public Task<List<HorarioMedicoDTO>> GetAllAsync(PaginationDTO pagination);
        public Task<Result<HorarioMedicoDTO>> GetByIdAsync(int id);
        public Task<Result<HorarioMedicoDTO>> CreateAsync(CreateHorarioMedicoDTO modeloDTO);
        public Task<Result> UpdateAsync(int id, UpdateHorarioMedicoDTO modeloDTO);
        public Task<Result> DeleteAsync(int id);
        Task<int> CountRecordsAsync();
    }
}
