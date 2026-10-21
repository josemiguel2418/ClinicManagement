using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class TipoAntecedenteMedicoRepository : ITipoAntecedenteMedicoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoAntecedenteMedicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TipoAntecedenteMedico?> GetByIdAsync(int id)
        {
            return await _context.TiposAntecedentesMedicos.FirstOrDefaultAsync(tp => tp.Id == id);
        }
    }
}
