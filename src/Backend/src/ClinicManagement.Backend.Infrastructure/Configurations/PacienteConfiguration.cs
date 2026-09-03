using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    internal class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(prop => prop.Apellido)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(prop => prop.Email).IsUnique();

            builder.Property(prop => prop.Telefono)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(prop => prop.TelefonoEmergencia)
               .HasMaxLength(50);

            builder.Property(prop => prop.Sexo)
                .HasConversion<string>()
                .HasMaxLength(30);
            builder.Property(prop => prop.TipoSangre)
                .HasConversion<string>()
                .HasMaxLength(30);
        }
    }
}
