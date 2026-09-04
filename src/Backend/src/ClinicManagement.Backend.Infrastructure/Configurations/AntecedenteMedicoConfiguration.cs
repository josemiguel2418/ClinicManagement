using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    internal class AntecedenteMedicoConfiguration : IEntityTypeConfiguration<AntecedenteMedico>
    {
        public void Configure(EntityTypeBuilder<AntecedenteMedico> builder)
        {
            builder.Property(prop => prop.Descripcion).IsRequired();
            builder
                .HasOne(a => a.TipoAntecedenteMedico)
                .WithMany()
                .HasForeignKey(a => a.TipoAntecedenteMedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
