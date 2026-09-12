using ClinicManagement.Backend.Business.DTOs;
using ClinicManagement.Backend.Domain.Entities;
using Mapster;

namespace ClinicManagement.Backend.Business.Mappings
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            ConfigureMedicoMappings();
        }

        private static void ConfigureMedicoMappings()
        {
            TypeAdapterConfig<Medico, MedicoDTO>.NewConfig();
            TypeAdapterConfig<MedicoEspecialidad, EspecialidadDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Especialidad.Id)
                .Map(dest => dest.Nombre, src => src.Especialidad.Nombre);
            TypeAdapterConfig<CreateMedicoDTO, Medico>.NewConfig()
                .Map(dest => dest.MedicoEspecialidades, src => src.EspecialidadesIds.Select(id => new MedicoEspecialidad { EspecialidadId = id }).ToList());
            TypeAdapterConfig<UpdateMedicoDTO, Medico>.NewConfig()
                .Map(dest => dest.MedicoEspecialidades, src => src.EspecialidadesIds.Select(id => new MedicoEspecialidad { EspecialidadId = id }).ToList());
        }
    }
}
