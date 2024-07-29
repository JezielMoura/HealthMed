namespace HealthMed.Domain.SchedulingAggregate;

public sealed class Scheduling
{
    public Guid Id { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid AvailabilityId { get; private set; }
    public DateTime Date { get; private set; }

    public Scheduling(Guid id, Guid doctorId, Guid patientId, Guid availabilityId, DateTime date)
    {
        Id = id;
        DoctorId = doctorId;
        PatientId = patientId;
        AvailabilityId = availabilityId;
        Date = date;
    }
}