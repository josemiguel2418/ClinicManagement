using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Medico medico)
        {
            _context.Medicos.Add(medico);
        }

        public Task<int> CountRecordsAsync()
        {
            return _context.Medicos.CountAsync();
        }

        public Task<List<Medico>> GetAllAsync(int pagina, int recordsPorPagina)
        {
            return _context.Medicos.Paginate(pagina, recordsPorPagina).OrderBy(m=>m.Nombre).Include(m=>m.TipoIdentificacion).Include(m=>m.MedicoEspecialidades).ThenInclude(me=>me.Especialidad).AsNoTracking().ToListAsync();
        }

        public Task<Medico?> GetByIdAsync(int id)
        {
            return _context.Medicos.Include(m => m.TipoIdentificacion).Include(m => m.MedicoEspecialidades).ThenInclude(me => me.Especialidad).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<Medico?> GetByIdentificacionAsync(string identificacion, int tipoIdentificacionId)
        {
            return _context.Medicos.Include(m => m.TipoIdentificacion).Include(m => m.MedicoEspecialidades).ThenInclude(me => me.Especialidad).FirstOrDefaultAsync(m => m.Identificacion == identificacion && m.TipoIdentificacionId == tipoIdentificacionId);
        }

        public Task<Medico?> GetByNombreAsync(string nombre)
        {
            return _context.Medicos.Include(m => m.TipoIdentificacion).Include(m => m.MedicoEspecialidades).ThenInclude(me => me.Especialidad).FirstOrDefaultAsync(m => m.Nombre == nombre);
        }
    }
}
