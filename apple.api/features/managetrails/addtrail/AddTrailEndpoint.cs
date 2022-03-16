using Ardalis.ApiEndpoints;
using apple.api.persistence.entities;
using apple.shared.features.managetrails.addtrail;
using Microsoft.AspNetCore.Mvc;

namespace apple.api.features.managetrails.addtrail;

public class AddTrailEndpoint : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithActionResult<int>
{
    private BlazingTrailsContext _context;

    public AddTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<int>> HandleAsync(AddTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = request.Trail.Name,
            Description = request.Trail.Description,
            Location = request.Trail.Location,
            TimeInMinutes = request.Trail.TimeInMinutes,
            Length = request.Trail.Length
        };

        await _context.AddAsync(trail, cancellationToken);

        var routeInstructions = request.Trail.Route.Select(x => new RouteInstruction
        {
            Stage = x.Stage,
            Description = x.Description
        });

        await _context.AddRangeAsync(routeInstructions, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Ok(trail.Id);
    }
}