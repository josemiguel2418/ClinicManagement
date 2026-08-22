using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
