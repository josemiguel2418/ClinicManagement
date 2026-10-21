using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;

namespace ClinicManagement.Backend.Infrastructure.Repositories
{
    public class AntecedenteMedicoRepository : IAntecedenteMedicoRepository
    {
        private readonly ApplicationDbContext _context;

        public AntecedenteMedicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Remove(AntecedenteMedico antecedente)
        {
            _context.AntecedentesMedico.Remove(antecedente);
        }
    }
}
