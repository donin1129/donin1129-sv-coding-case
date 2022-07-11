using SvCodingCase.Application.Common.Interfaces;
using MediatR;
using System.Net;
using System.Web.Http;

namespace SvCodingCase.Application.Commands;

public record RequestLicense : INotification
{
}

public class RequestLicenseHandler : INotificationHandler<RequestLicense>
{
    private readonly ILicenseGeneratorService _licenseGeneratorService;
    private readonly ICurrentUserService _currentUserService;

    public RequestLicenseHandler(ILicenseGeneratorService licenseGeneratorService, ICurrentUserService currentUserService)
    {
        _licenseGeneratorService = licenseGeneratorService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(RequestLicense request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        await _licenseGeneratorService.GenerateLicenseRequest(userId);
    }
}
