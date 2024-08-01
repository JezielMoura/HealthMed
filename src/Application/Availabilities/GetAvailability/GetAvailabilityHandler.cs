using HealthMed.Domain.AvailabilityAggregate;
using MediatR;

namespace HealthMed.Application.Availabilities.GetAvailability;

public sealed class GetAvailabilityHandler : IRequestHandler<GetAvailabilityQuery, IEnumerable<AvailabilityResponse>>
{
    private readonly IAvailabilityRepository _availabilityRepository;

    public GetAvailabilityHandler(IAvailabilityRepository availabilityRepository)
    {
        _availabilityRepository = availabilityRepository;
    }

    public async Task<IEnumerable<AvailabilityResponse>> Handle(GetAvailabilityQuery query, CancellationToken cancellationToken)
    {
        var availabilities = await _availabilityRepository.Get(query.Page, query.Limit);
        var groupByDoctor = availabilities.GroupBy(x => x.DoctorId);
        var allAvailabilities = groupByDoctor.Select(AvailabilityResponse.Create);

        return allAvailabilities;
    }
}
