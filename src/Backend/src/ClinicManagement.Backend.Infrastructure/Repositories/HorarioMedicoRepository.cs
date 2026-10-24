using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class HorarioMedicoRepository : IHorarioMedicoRepository
    {
        private readonly ApplicationDbContext _context;

        public HorarioMedicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(HorarioMedico horarioMedico)
        {
            _context.HorariosMedicos.Add(horarioMedico);
        }

        public async Task<int> CountRecordsAsync()
        {
            return await _context.HorariosMedicos.CountAsync();
        }

        public async Task<List<HorarioMedico>> GetAllAsync(int pagina, int recordsPorPagina)
        {
            return await _context.HorariosMedicos.AsNoTracking().Include(hm => hm.Medico).ThenInclude(m => m.MedicoEspecialidades)
                .ThenInclude(me => me.Especialidad).Include(hm => hm.Consultorio).OrderBy(hm => hm.HoraInicio).ThenBy(hm => hm.HoraFin).Paginate(pagina, recordsPorPagina).ToListAsync();
        }

        public async Task<HorarioMedico?> GetByIdAsync(int id)
        {
            return await _context.HorariosMedicos.Include(hm => hm.Medico).ThenInclude(m => m.MedicoEspecialidades)
                .ThenInclude(me => me.Especialidad).Include(hm => hm.Consultorio).FirstOrDefaultAsync(horarioMedico => horarioMedico.Id == id);
        }

        public Task<bool> HasConflictAsync(
            int medicoId,
            int consultorioId,
            Dia dia,
            TimeOnly horaInicio,
            TimeOnly horaFin,
            int? excludedId = null)
        {
            return _context.HorariosMedicos.AnyAsync(h =>
                h.Dia == dia &&
                h.Activo &&
                h.HoraInicio < horaFin &&
                h.HoraFin > horaInicio &&
                (!excludedId.HasValue || h.Id != excludedId.Value) &&
                (
                    h.MedicoId == medicoId ||
                    h.ConsultorioId == consultorioId
                ));
        }
        public Task<bool> HasActiveScheduleCoveringAsync(
            int medicoId,
            Dia dia,
            TimeOnly horaInicio,
            TimeOnly horaFin)
        {
            return _context.HorariosMedicos.AnyAsync(h =>
                h.MedicoId == medicoId &&
                h.Dia == dia &&
                h.Activo &&
                h.Medico.Activo &&
                h.Consultorio.Activo &&
                h.HoraInicio <= horaInicio &&
                h.HoraFin >= horaFin);
        }
    }
}
