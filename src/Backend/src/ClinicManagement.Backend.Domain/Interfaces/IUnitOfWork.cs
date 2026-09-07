namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get; }
        public IPacienteRepository PacienteRepository { get; }
        Task<int> Save();
    }
}
