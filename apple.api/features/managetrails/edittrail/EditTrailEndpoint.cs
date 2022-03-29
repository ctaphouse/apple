using apple.shared.features.managedtrails.edittrail;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using apple.api.persistence.entities;
using apple.shared.features.managetrails.shared;

namespace apple.api.features.managetrails.edittrail;

public class EditTrailEndpoint : EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<bool>
{
    private BlazingTrailsContext _context;
    public EditTrailEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var trail = await _context.Trails.Include(x => x.Route).SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken: cancellationToken);

            if (trail is null)
            {
                return BadRequest("Trail could not be found.");
            }

            trail.Name = request.Trail.Name;
            trail.Description = request.Trail.Description;
            trail.Location = request.Trail.Location;
            trail.TimeInMinutes = request.Trail.TimeInMinutes;
            trail.Image = request.Trail.Image;
            trail.Length = request.Trail.Length;
            trail.Route = request.Trail.Route.Select(x => new RouteInstruction { Stage = x.Stage, Description = x.Description, Trail = trail }).ToList();

            if(request.Trail.ImageAction == ImageAction.Remove)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Image", trail.Image!));
                trail.Image = null;
            }

            await _context.SaveChangesAsync();

            return Ok(true);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}