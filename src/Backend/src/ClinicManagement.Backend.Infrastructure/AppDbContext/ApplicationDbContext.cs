using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Backend.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<TipoIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<TipoAntecedenteMedico> TiposAntecedentesMedicos { get; set; }
        public DbSet<AntecedenteMedico> AntecedentesMedico { get; set; }
        public DbSet<Medico> Medicos { get; set; }
    }
}
