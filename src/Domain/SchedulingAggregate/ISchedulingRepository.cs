namespace HealthMed.Domain.SchedulingAggregate;

public interface ISchedulingRepository
{
    Task Add(Scheduling scheduling);
}