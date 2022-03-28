using apple.shared.features.managetrails.shared;
using FluentValidation;
using MediatR;

namespace apple.shared.features.managedtrails.edittrail;

public record EditTrailRequest(TrailDto Trail) : IRequest<EditTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails/edit-trail/{Trail.Id}";
    public record Response(bool IsSuccess);
}

public class EditTrailRequestValidator : AbstractValidator<EditTrailRequest>
{
    public EditTrailRequestValidator()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}