using apple.shared.features.managedtrails.edittrail;
using MediatR;
using System.Net.Http.Json;

namespace apple.client.features.managetrails.edittrail;

public class EditTrailHandler : IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    private HttpClient _httpClient;

    public EditTrailHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

            if(response.IsSuccessStatusCode)
            {
                return new EditTrailRequest.Response(true);
            }
            else
            {
                return new EditTrailRequest.Response(false);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}