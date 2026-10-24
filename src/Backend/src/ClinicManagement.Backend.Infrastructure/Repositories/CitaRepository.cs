using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public CitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Cita cita)
        {
            _context.Citas.Add(cita);
        }

        public async Task<int> CountRecordsAsync()
        {
            return await _context.Citas.CountAsync();
        }

        public async Task<List<Cita>> GetAllAsync(
            int pagina,
            int recordsPorPagina)
        {
            return await _context.Citas
                .AsNoTracking()
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.HoraInicio)
                .Include(c=>c.Paciente)
                .Include(c=>c.Medico)
                .Paginate(pagina, recordsPorPagina)
                .ToListAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> HasConflictAsync(
            int medicoId,
            int pacienteId,
            DateOnly fecha,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            int? excludedId = null)
        {
            return _context.Citas.AnyAsync(c =>
                c.Fecha == fecha &&
                c.EstadoCita != EstadoCita.Cancelada &&
                (!excludedId.HasValue || c.Id != excludedId.Value) &&
                (c.MedicoId == medicoId ||
                 c.PacienteId == pacienteId) &&
                c.HoraInicio < horaFin &&
                c.HoraFin > horaInicio);
        }
    }
}
