using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;

namespace ClinicManagement.Backend.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get;}
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, IConsultorioRepository consultorioRepository)
        {
            _context = context;
            ConsultorioRepository = consultorioRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

    }
}
