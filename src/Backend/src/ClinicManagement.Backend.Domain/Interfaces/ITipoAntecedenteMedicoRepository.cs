using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface ITipoAntecedenteMedicoRepository
    {
        public Task<TipoAntecedenteMedico?> GetByIdAsync(int id);
    }
}
