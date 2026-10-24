using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Mappings;
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
            services.AddScoped<IEspecialidadService,EspecialidadService>();
            services.AddScoped<IMedicoService,MedicoService>();
            services.AddScoped<IHorarioMedicoService, HorarioMedicoService>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddMapster();
            MappingConfig.RegisterMappings();
            return services;
        }
    }
}
