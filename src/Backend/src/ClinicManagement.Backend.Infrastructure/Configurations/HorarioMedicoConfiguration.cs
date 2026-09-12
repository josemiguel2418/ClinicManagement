using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    public class HorarioMedicoConfiguration : IEntityTypeConfiguration<HorarioMedico>
    {
        public void Configure(EntityTypeBuilder<HorarioMedico> builder)
        {
            builder.ToTable("HorariosMedicos");

            builder.Property(prop => prop.Dia)
                .HasConversion<string>();

            builder.HasOne(x => x.Medico)
           .WithMany()
           .HasForeignKey(x => x.MedicoId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Consultorio)
            .WithMany()
            .HasForeignKey(x => x.ConsultorioId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(prop => prop.Activo)
                .HasDefaultValue(true);

            builder.ToTable(t => t.HasCheckConstraint("CK_HorarioMedico_HoraInicio_HoraFin",
        "[HoraInicio] < [HoraFin]"));
        }
    }
}
