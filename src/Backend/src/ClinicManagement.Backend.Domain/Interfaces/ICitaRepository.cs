using ClinicManagement.Backend.Domain.Entities;

namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface ICitaRepository
    {
        void Add(Cita cita);

        Task<int> CountRecordsAsync();

        Task<List<Cita>> GetAllAsync(
            int pagina,
            int recordsPorPagina);

        Task<Cita?> GetByIdAsync(int id);

        Task<bool> HasConflictAsync(
            int medicoId,
            int pacienteId,
            DateOnly fecha,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            int? excludedId = null);

    }
}
