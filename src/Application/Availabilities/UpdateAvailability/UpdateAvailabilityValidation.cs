using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace HealthMed.Application.Availabilities.UpdateAvailability;

[ExcludeFromCodeCoverage]
public sealed class UpdateAvailabilityValidation : AbstractValidator<UpdateAvailabilityCommand>
{
    public UpdateAvailabilityValidation()
    {
        RuleFor(p => p)
            .Must((args) => ValidRange(args.From, args.To, args.DurationInMinutes))
            .WithMessage($"Put a datetime range valid for the availability duration");
    }

    private static bool ValidRange(DateTimeOffset from, DateTimeOffset to, int durationInMinutes)
    {
        var totalMinutes = (to - from).TotalMinutes;
        var totalAvailability = Math.Ceiling(totalMinutes / durationInMinutes);
        var dateRangeIsValid = totalAvailability * durationInMinutes == totalMinutes;

        return dateRangeIsValid;
    }
}