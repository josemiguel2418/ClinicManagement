using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IEspecialidadRepository
    {
        public void Add(Especialidad especialidad);
        public Task<Especialidad?> GetByIdAsync(int id);
        public Task<Especialidad?> GetByNombreAsync(string nombre);
        public Task<List<Especialidad>> GetAllAsync(int pagina, int recordsPorPagina);
        public Task<int> CountRecordsAsync();
        public void Delete(Especialidad especialidad);
    }
}
