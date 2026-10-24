using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Business.Results;

namespace ClinicManagement.Backend.Business.Interfaces
{
    public interface ICitaService
    {
        Task<int> CountRecordsAsync();

        Task<List<CitaDTO>> GetAllAsync(PaginationDTO pagination);

        Task<Result<CitaDTO>> GetByIdAsync(int id);

        Task<Result<CitaDTO>> CreateAsync(CreateCitaDTO modeloDTO);

        Task<Result> UpdateAsync(int id,UpdateCitaDTO modeloDTO);

        Task<Result> DeleteAsync(int id);
    }
}
