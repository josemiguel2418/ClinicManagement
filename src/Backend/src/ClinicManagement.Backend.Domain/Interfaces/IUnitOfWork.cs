namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get; }
        public IPacienteRepository PacienteRepository { get; }
        public IEspecialidadRepository EspecialidadRepository { get; }
        public IMedicoRepository MedicoRepository { get; }
        public IHorarioMedicoRepository HorarioMedicoRepository { get; }
        public ITipoAntecedenteMedicoRepository TipoAntecedenteMedicoRepository { get; }
        public IAntecedenteMedicoRepository AntecedenteMedicoRepository { get; }
        Task<int> Save();
    }
}
