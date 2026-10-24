using ClinicManagement.Backend.Domain.Interfaces;
using ClinicManagement.Backend.Infrastructure.AppDbContext;
using ClinicManagement.Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("name=DefaultConnection");
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IConsultorioRepository, ConsultorioRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IHorarioMedicoRepository, HorarioMedicoRepository>();
            services.AddScoped<IAntecedenteMedicoRepository, AntecedenteMedicoRepository>();
            services.AddScoped<ITipoAntecedenteMedicoRepository, TipoAntecedenteMedicoRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();
            return services;
        }
    }
}
