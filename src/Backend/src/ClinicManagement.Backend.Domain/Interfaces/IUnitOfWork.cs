namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IConsultorioRepository ConsultorioRepository { get; }
        Task<int> Save();
    }
}
