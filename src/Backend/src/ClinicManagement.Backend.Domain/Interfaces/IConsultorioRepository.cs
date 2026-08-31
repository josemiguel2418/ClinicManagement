using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IConsultorioRepository
    {
        public void Add(Consultorio consultorio);
        public Task<Consultorio?> GetByIdAsync(int id);
        public Task<List<Consultorio>> GetAllAsync(int pagina, int recordsPorPagina);
        public Task<Consultorio?> GetByNameAsync(string name);
        public Task<int> CountRecordsAsync();
    }
}
