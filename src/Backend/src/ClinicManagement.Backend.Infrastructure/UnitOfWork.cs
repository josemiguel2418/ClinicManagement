using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;

namespace ClinicManagement.Backend.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get;}
        public IPacienteRepository PacienteRepository { get;}
        public IEspecialidadRepository EspecialidadRepository { get;}
        public IMedicoRepository MedicoRepository { get;}
        public IHorarioMedicoRepository HorarioMedicoRepository { get; }
        public ITipoAntecedenteMedicoRepository TipoAntecedenteMedicoRepository { get; }
        public IAntecedenteMedicoRepository AntecedenteMedicoRepository { get; }
        public ICitaRepository CitaRepository { get; }

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, IConsultorioRepository consultorioRepository,IPacienteRepository pacienteRepository,IEspecialidadRepository especialidadRepository, IMedicoRepository medicoRepository,IHorarioMedicoRepository horarioMedicoRepository, ITipoAntecedenteMedicoRepository tipoAntecedenteMedicoRepository, IAntecedenteMedicoRepository antecedenteMedicoRepository,ICitaRepository citaRepository)
        {
            _context = context;
            ConsultorioRepository = consultorioRepository;
            PacienteRepository = pacienteRepository;
            EspecialidadRepository = especialidadRepository;
            MedicoRepository = medicoRepository;
            HorarioMedicoRepository = horarioMedicoRepository;
            TipoAntecedenteMedicoRepository = tipoAntecedenteMedicoRepository;
            AntecedenteMedicoRepository = antecedenteMedicoRepository;
            CitaRepository = citaRepository;
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();

    }
}
