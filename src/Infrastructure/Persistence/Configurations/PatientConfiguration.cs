using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Persistence.Configurations;

[ExcludeFromCodeCoverage]
internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .ToTable("Patients");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Name)
            .HasColumnType("varchar(120)");

        builder
            .Property(p => p.Email)
            .HasColumnType("varchar(120)");

        builder
            .Property(p => p.CPF)
            .HasColumnType("varchar(20)");
            
        builder
            .Property(p => p.Password)
            .HasColumnType("varchar(100)");
    }
}