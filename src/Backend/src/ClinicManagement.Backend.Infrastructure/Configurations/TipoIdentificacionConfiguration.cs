using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    internal class TipoIdentificacionConfiguration : IEntityTypeConfiguration<TipoIdentificacion>
    {
        public void Configure(EntityTypeBuilder<TipoIdentificacion> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(prop => prop.Nombre)
                .IsUnique();
        }
    }
}
