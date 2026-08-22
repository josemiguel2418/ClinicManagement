using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    internal class ConsultorioConfiguration : IEntityTypeConfiguration<Consultorio>
    {
        public void Configure(EntityTypeBuilder<Consultorio> builder)
        {
            builder.Property(prop => prop.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(prop => prop.Nombre).IsUnique();

        }
    }
}
