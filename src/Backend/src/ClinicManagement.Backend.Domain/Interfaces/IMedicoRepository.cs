using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IMedicoRepository
    {
        public void Add(Medico medico);
        public Task<List<Medico>> GetAllAsync(int pagina,int recordsPorPagina);
        public Task<Medico?> GetByIdAsync(int id);
        public Task<Medico?> GetByIdentificacionAsync(string identificacion,int tipoIdentificacionId);
        public Task<Medico?> GetByNombreAsync(string nombre);
        public Task<int> CountRecordsAsync();
    }
}
