namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get; }
        public IPacienteRepository PacienteRepository { get; }
        public IEspecialidadRepository EspecialidadRepository { get; }
        public IMedicoRepository MedicoRepository { get; }

        Task<int> Save();
    }
}
