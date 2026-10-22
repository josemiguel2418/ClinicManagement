using ClinicManagement.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicManagement.Backend.Infrastructure.Configurations
{
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
               "CK_Citas_HoraInicio_HoraFin",
               "[HoraInicio] < [HoraFin]");

                t.HasCheckConstraint(
                                "CK_Citas_EstadoCita",
                                "[EstadoCita] IN " +
                                "('Programada', 'Confirmada', 'Completada', 'Cancelada', 'NoAsistio')");
            });

            builder.Property(c => c.Fecha).IsRequired();

            builder.Property(c => c.EstadoCita)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.HoraInicio)
                .HasColumnType("time(0)")
                .IsRequired();

            builder.Property(c => c.HoraFin)
                .HasColumnType("time(0)")
                .IsRequired();

            builder.Property(c => c.Motivo)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
