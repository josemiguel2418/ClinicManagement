using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    internal class TipoAntecedenteMedicoConfiguration : IEntityTypeConfiguration<TipoAntecedenteMedico>
    {
        public void Configure(EntityTypeBuilder<TipoAntecedenteMedico> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(prop=>prop.Nombre)
                .IsUnique();
        }
    }
}
