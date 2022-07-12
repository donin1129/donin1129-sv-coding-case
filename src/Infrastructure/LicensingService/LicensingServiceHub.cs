using System.Diagnostics;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.Domain.Entities;

namespace SvCodingCase.Infrastructure.LicensingService;

/*[Authorize]*/
public class LicensingServiceHub : Hub, ILicenseGeneratorService
{

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubContext<LicensingServiceHub> _context;

    public LicensingServiceHub(IServiceScopeFactory serviceScopeFactory, IHubContext<LicensingServiceHub> context)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _context = context;
    }

    public async Task OnLicenseGenerated(Tuple<string, string> idLicensePair)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<LicensingServiceHub>>();

            logger.LogInformation($"User with id {idLicensePair.Item1} get new generated license: {idLicensePair.Item2}");
            if (idLicensePair.Item1 is null)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            var savedEntry = dbContext.Licenses.FirstOrDefault(p => p.UserId == idLicensePair.Item1);

            // If the client already have a license
            if (savedEntry is not null)
            {
                savedEntry.LatestLicense = idLicensePair.Item2;
                dbContext.Licenses.Update(savedEntry);
            }
            // If the client never had the license
            else
            {
                dbContext.Licenses.Add(new License() { UserId = idLicensePair.Item1, LatestLicense = idLicensePair.Item2 });
            }
            
            await dbContext.SaveChangesAsync(default);
        }
    }

    public async Task GenerateLicenseRequest(string userid)
    {
        await _context.Clients.All.SendAsync("GenerateLicense", userid);
    }

    public Task<string> RetrieveDatabaseEntryIfCreated(string userid, TimeSpan timeSpan)
    {
        if (timeSpan <= new TimeSpan(0, 0, 0 ,0))
        {
            return null;
        }

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<LicensingServiceHub>>();
            var currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();

            logger.LogInformation($"Checking if database entry is created for {userid} the maximum waiting time is {timeSpan}");
            
            var licenseEntry = dbContext.Licenses.FirstOrDefault(l => l.UserId == userid);

            if (licenseEntry is null)
            {
                Task.Delay(500); // Every 0.5 second check if the value is added to the db. 
                return RetrieveDatabaseEntryIfCreated(userid, timeSpan - new TimeSpan(0, 0, 0, 0, 500));
            }

            else // licenseEntry is not null
            {
                return Task.FromResult(licenseEntry.LatestLicense);
            }

        }
    }
}