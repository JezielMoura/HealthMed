using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Persistence.Configurations;

internal sealed class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder
            .ToTable("Availabilities");

        builder
            .HasKey(p => p.Id);

        builder
            .HasOne<Doctor>()
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(e => e.DoctorId)
            .IsRequired();

        builder
            .Property(p => p.DoctorName)
            .HasColumnType("varchar(120)");
    }
}