namespace HealthMed.Domain.DoctorAggregate;

public sealed class Availability
{
    public Guid Id { get; private set; }
    public Guid DoctorId { get; private set; }
    public DateTime DateTime { get; private set; }
    public bool IsAvailable { get; private set; }

    public Availability(Guid id, Guid doctorId, DateTime dateTime, bool isAvailable)
    {
        Id = id;
        DoctorId = doctorId;
        DateTime = dateTime;
        IsAvailable = isAvailable;
    }
}