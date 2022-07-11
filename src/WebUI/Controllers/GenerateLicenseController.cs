using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvCodingCase.Application.Commands;

namespace SvCodingCase.WebUI.Controllers;

[Authorize]
public class LicenseController : ApiControllerBase
{
    [HttpPost]
    public async Task GenerateLicense()
    {
       await Mediator.Publish(new RequestLicense());
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetLatestLicense()
    {
        return await Mediator.Send(new RetrieveLatestLicense());
    }
}
