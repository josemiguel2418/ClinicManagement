using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    public class SignosVitalesConfiguration : IEntityTypeConfiguration<SignosVitales>
    {
        public void Configure(EntityTypeBuilder<SignosVitales> builder)
        {
            builder.ToTable("SignosVitales", table =>
            {
                table.HasCheckConstraint(
                    "CK_SignosVitales_AlturaPositiva",
                    "[Altura] IS NULL OR [Altura] > 0");

                table.HasCheckConstraint(
                    "CK_SignosVitales_PesoPositivo",
                    "[Peso] IS NULL OR [Peso] > 0");

                table.HasCheckConstraint(
                    "CK_SignosVitales_FrecuenciaCardiacaPositiva",
                    "[FrecuenciaCardiaca] IS NULL OR [FrecuenciaCardiaca] >= 0");

                table.HasCheckConstraint(
                    "CK_SignosVitales_TemperaturaPositiva",
                    "[Temperatura] IS NULL OR [Temperatura] > 0");

                table.HasCheckConstraint(
                    "CK_SignosVitales_PresionPositiva",
                    """
                ([PresionSistolica] IS NULL OR [PresionSistolica] > 0)
                AND
                ([PresionDiastolica] IS NULL OR [PresionDiastolica] > 0)
                """);

                table.HasCheckConstraint(
                    "CK_SignosVitales_PresionCompleta",
                    """
                ([PresionSistolica] IS NULL AND [PresionDiastolica] IS NULL)
                OR
                ([PresionSistolica] IS NOT NULL AND [PresionDiastolica] IS NOT NULL)
                """);

                table.HasCheckConstraint(
                    "CK_SignosVitales_AlMenosUnaMedicion",
                    """
                [Altura] IS NOT NULL
                OR [Peso] IS NOT NULL
                OR [FrecuenciaCardiaca] IS NOT NULL
                OR [Temperatura] IS NOT NULL
                OR [PresionSistolica] IS NOT NULL
                """);
            });

            builder.Property(signosVitales => signosVitales.Altura)
                .HasPrecision(5, 2);

            builder.Property(signosVitales => signosVitales.Peso)
                .HasPrecision(5, 2);

            builder.Property(signosVitales => signosVitales.Temperatura)
                .HasPrecision(5, 2);

            builder.Property(signosVitales => signosVitales.FechaCreacion)
                .IsRequired();

            builder.HasOne(signosVitales => signosVitales.Consulta)
                .WithOne(consulta => consulta.SignosVitales)
                .HasForeignKey<SignosVitales>(
                    signosVitales => signosVitales.ConsultaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
