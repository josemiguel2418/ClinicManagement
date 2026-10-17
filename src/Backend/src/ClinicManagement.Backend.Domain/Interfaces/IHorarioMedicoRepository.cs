using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IHorarioMedicoRepository
    {
        public void Add(HorarioMedico horarioMedico);
        public Task<HorarioMedico?> GetByIdAsync(int id);
        public Task<bool> HasConflictAsync(
            int medicoId,
            int consultorioId,
            Dia dia,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            int? excludedId = null);
        public Task<List<HorarioMedico>> GetAllAsync(int pagina, int recordsPorPagina);
        public Task<int> CountRecordsAsync();
    }
}
