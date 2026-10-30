using ClinicManagement.Backend.Domain.Entities;
using ClinicManagement.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.Property(consulta => consulta.CitaId)
            .IsRequired();
            
            builder.Property(consulta => consulta.Estado)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();


            builder.Property(consulta => consulta.FechaCreacion)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasOne(consulta => consulta.Cita)
                   .WithOne()
                   .HasForeignKey<Consulta>(consulta => consulta.CitaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
