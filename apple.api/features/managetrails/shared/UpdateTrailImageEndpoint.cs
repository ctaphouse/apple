using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using apple.shared.features.managetrails.shared;

namespace apple.api.features.managetrails.shared;
public class UpdateTrailImageEndpoint : EndpointBaseAsync.WithRequest<int>.WithActionResult<bool>
{
    private readonly BlazingTrailsContext _context;

    public UpdateTrailImageEndpoint(BlazingTrailsContext context)
    {
        _context = context;
    }

    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await _context.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);

        if(trail is null)
        {
            return BadRequest("Trail does not exist");
        }

        var file = Request.Form.Files[0];

        if(file.Length == 0)
        {
            return BadRequest("No image found");
        }

        var fileName = $"{Guid.NewGuid()}.jpg";

        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);

        var resizeOptions = new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new Size(640, 426)
        };

        using var image = Image.Load(file.OpenReadStream());
        
        image.Mutate(x => x.Resize(resizeOptions));
        
        await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

        trail.Image = fileName;

        await _context.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}