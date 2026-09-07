using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Backend.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) 
        {
            services.AddScoped<IConsultorioService,ConsultorioService>();
            services.AddScoped<IPacienteService,PacienteService>();
            services.AddMapster();
            return services;
        }
    }
}
