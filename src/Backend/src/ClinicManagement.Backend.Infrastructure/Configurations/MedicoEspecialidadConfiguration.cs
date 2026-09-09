using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    public class MedicoEspecialidadConfiguration : IEntityTypeConfiguration<MedicoEspecialidad>
    {
        public void Configure(EntityTypeBuilder<MedicoEspecialidad> builder)
        {
            builder.HasKey(me => new
            {
                me.MedicoId,
                me.EspecialidadId
            });

            builder.HasOne(me => me.Medico)
                .WithMany(m => m.MedicoEspecialidades)
                .HasForeignKey(me => me.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(me => me.Especialidad)
                .WithMany()
                .HasForeignKey(me => me.EspecialidadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

