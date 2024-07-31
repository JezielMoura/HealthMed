using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Persistence.Configurations;

internal sealed class SchedulingConfiguration : IEntityTypeConfiguration<Scheduling>
{
    public void Configure(EntityTypeBuilder<Scheduling> builder)
    {
        builder
            .ToTable("Schedulings");

        builder
            .HasKey(p => p.Id);

        builder
            .HasOne<Availability>()
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey<Scheduling>(e => e.AvailabilityId)
            .IsRequired();

        builder
            .HasOne<Doctor>()
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(e => e.DoctorId)
            .IsRequired();

        builder
            .HasOne<Patient>()
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(e => e.PatientId)
            .IsRequired();
    }
}