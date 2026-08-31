using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class ConsultorioRepository : IConsultorioRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultorioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Consultorio>> GetAllAsync(int pagina,int recordsPorPagina)
        {
            return await _context.Consultorios.Paginate(pagina,recordsPorPagina).OrderBy(consultorio=>consultorio.Nombre).AsNoTracking().ToListAsync();
        }

        public async Task<Consultorio?> GetByIdAsync(int id)
        {
            return await _context.Consultorios.FirstOrDefaultAsync(consultorio => consultorio.Id == id);
        }
        public void Add(Consultorio consultorio)
        {
            _context.Add(consultorio);
        }
        public async Task<Consultorio?> GetByNameAsync(string name)
        {
            return await _context.Consultorios.FirstOrDefaultAsync(consultorio => consultorio.Nombre == name);
        }
        public async Task<int> CountRecordsAsync()
        {
            return await _context.Consultorios.CountAsync();
        }
    }
}
