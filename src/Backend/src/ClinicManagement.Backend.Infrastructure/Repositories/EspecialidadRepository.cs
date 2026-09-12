using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class EspecialidadRepository : IEspecialidadRepository
    {
        private readonly ApplicationDbContext _context;
        public EspecialidadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Especialidad especialidad)
        {
            _context.Especialidades.Add(especialidad);
        }

        public async Task<int> CountRecordsAsync()
        {
            return await _context.Especialidades.CountAsync();
        }

        public async Task<List<Especialidad>> GetAllAsync(int pagina, int recordsPorPagina)
        {
            return await _context.Especialidades.Paginate(pagina, recordsPorPagina).OrderBy(e => e.Nombre).AsNoTracking().ToListAsync();
        }

        public async Task<Especialidad?> GetByIdAsync(int id)
        {
            return await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<int>> GetExistingIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Especialidades.Where(e => ids.Contains(e.Id)).Select(e=>e.Id).ToListAsync();
        }

        public async Task<Especialidad?> GetByNombreAsync(string nombre)
        {
            return await _context.Especialidades.FirstOrDefaultAsync(e => e.Nombre == nombre);
        }
        public void Delete(Especialidad especialidad)
        {
            _context.Especialidades.Remove(especialidad);
        }
    }
}
