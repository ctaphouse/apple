using apple.shared.features.managedtrails.edittrail;
using MediatR;
using System.Net.Http.Json;

namespace apple.client.features.managetrails.edittrail;

public class GetTrailHandler : IRequestHandler<GetTrailRequest, GetTrailRequest.Response?>
{
    private HttpClient _httpclient;

    public GetTrailHandler(HttpClient httpClient)
    {
       _httpclient = httpClient;
    }

    public async Task<GetTrailRequest.Response?> Handle(GetTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await _httpclient.GetFromJsonAsync<GetTrailRequest.Response>(GetTrailRequest.RouteTemplate.Replace("{_trail.Id}", request.TrailId.ToString()));    
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}