using ClinicManagement.Backend.Business.Interfaces;
using ClinicManagement.Backend.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManagement.Backend.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) 
        {
            services.AddScoped<IConsultorioService,ConsultorioService>();
            return services;
        }
    }
}
