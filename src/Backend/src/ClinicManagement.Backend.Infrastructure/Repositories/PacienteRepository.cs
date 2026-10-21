using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {

        private readonly ApplicationDbContext _context;
        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Paciente paciente)
        {
            _context.Add(paciente);
        }

        public async Task<int> CountRecordsAsync()
        {
            return await _context.Pacientes.CountAsync();
        }

        public async Task<List<Paciente>> GetAllAsync(int pagina, int recordsPorPagina)
        {
            return await _context.Pacientes.Paginate(pagina, recordsPorPagina).OrderBy(p => p.Nombre).Include(p => p.TipoIdentificacion).AsNoTracking().ToListAsync();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.Where(p=>p.Id == id).Include(p => p.TipoIdentificacion).Include(p=>p.AntecedentesMedicos).FirstOrDefaultAsync();
        }

        public async Task<Paciente?> GetByIdentificacionAsync(string identificacion,int tipoIdentificacionId)
        {
            return await _context.Pacientes.Where(p => p.Identificacion == identificacion && p.TipoIdentificacionId == tipoIdentificacionId).Include(p=>p.TipoIdentificacion).FirstOrDefaultAsync();
        }
    }
}
