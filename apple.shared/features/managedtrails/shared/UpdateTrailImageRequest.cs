using Microsoft.AspNetCore.Components.Forms;
using MediatR;

namespace apple.shared.features.managetrails.shared;
public record UploadTrailImageRequest(int TrailId, IBrowserFile File) : IRequest<UploadTrailImageRequest.Response>
{
    public const string RouteTemplate = "api/trails/{trailId}/images";

    public record Response(bool IsSuccess);
}