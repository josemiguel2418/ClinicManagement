using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;

namespace ClinicManagement.Backend.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get;}
        public IPacienteRepository PacienteRepository { get;}
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, IConsultorioRepository consultorioRepository,IPacienteRepository pacienteRepository)
        {
            _context = context;
            ConsultorioRepository = consultorioRepository;
            PacienteRepository = pacienteRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

    }
}
