using SvCodingCase.Application.Common.Interfaces;
using MediatR;
using System.Web.Http;
using System.Net;

namespace SvCodingCase.Application.Commands;

public record RetrieveLatestLicense : IRequest<string>
{
}

public class RetrieveLatestLicenseHandler : IRequestHandler<RetrieveLatestLicense, string>
{
    private readonly ILicenseGeneratorService _licenseGeneratorService;
    private readonly ICurrentUserService _currentUserService;

    public RetrieveLatestLicenseHandler(ILicenseGeneratorService licenseGeneratorService, ICurrentUserService currentUserService)
    {
        _licenseGeneratorService = licenseGeneratorService;
        _currentUserService = currentUserService;
    }

    public async Task<string> Handle(RetrieveLatestLicense request, CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId is null)
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        var savedLicenseInDB = _licenseGeneratorService.RetrieveDatabaseEntryIfCreated(_currentUserService.UserId, new TimeSpan(0, 0, 0, 0, 5));

        if (savedLicenseInDB is null)
        {
            await _licenseGeneratorService.GenerateLicenseRequest(_currentUserService.UserId);
            return await _licenseGeneratorService.RetrieveDatabaseEntryIfCreated(_currentUserService.UserId, new TimeSpan(0, 0, 10));
        }
        else
        {
            return await savedLicenseInDB;
        }
    }
}
