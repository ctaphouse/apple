using FluentValidation;
using MediatR;
using apple.shared.features.managetrails.shared;

namespace apple.shared.features.managetrails.addtrail;
public record AddTrailRequest(TrailDto Trail) : IRequest<AddTrailRequest.Response>
{
    public const string RouteTemplate = "/add/trails";
    public record Response(int TrailId);
}

public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
public AddTrailRequestValidator()
{
    RuleFor(x => x.Trail).SetValidator(new TrailValidator());
}
}
