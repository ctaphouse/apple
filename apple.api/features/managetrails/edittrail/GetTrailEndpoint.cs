using apple.shared.features.managedtrails.edittrail;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apple.api.features.managetrails.edittrail;

public class GetTrailEndpoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailRequest.Response>
{  
    private BlazingTrailsContext _context;

    public GetTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails.Include(x => x.Route).SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken: cancellationToken);

        if (trail is null)
        {
            return BadRequest("Trail could not be found");
        }

        var response = new GetTrailRequest.Response(new GetTrailRequest.Trail(trail.Id,
        trail.Name, trail.Location, trail.Image, trail.TimeInMinutes, trail.Length, trail.Description,
        trail.Route.Select(x => new GetTrailRequest.RouteInstruction(x.Id, x.Stage, x.Description))));

        return Ok(response);
    }
}