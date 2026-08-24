namespace ClinicManagement.Backend.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Save();
    }
}
