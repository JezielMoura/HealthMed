namespace HealthMed.Domain.AvailabilityAggregate;

public sealed class Availability
{
    public Guid Id { get; private set; }
    public Guid DoctorId { get; private set; }
    public string DoctorName { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTimeOffset DateTime { get; private set; }

    public Availability(Guid id, Guid doctorId, string doctorName, bool isAvailable, DateTime dateTime)
    {
        Id = id;
        DoctorId = doctorId;
        DoctorName = doctorName;
        IsAvailable = isAvailable;
        DateTime = dateTime;
    }

    public void IsUnavailable() =>
        IsAvailable = false;

    #nullable disable
    private Availability() {}
}