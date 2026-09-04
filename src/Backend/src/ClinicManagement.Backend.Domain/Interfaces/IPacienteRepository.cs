using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IPacienteRepository
    {
        public void Add(Paciente paciente);
        public Task<Paciente?> GetByIdAsync(int id);
        public Task<List<Paciente>> GetAllAsync(int pagina, int recordsPorPagina);
        public Task<Paciente?> GetByIdentificacionAsync(string identificacion, int tipoIdentificacionId);
        public Task<int> CountRecordsAsync();
    }
}
