using SvCodingCase.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvCodingCase.DTOs;
using SvCodingCase.Application.Queries;

namespace SvCodingCase.WebUI.Controllers;

[Authorize]
public class SearchEngineController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DataFileDto>> GetDataFileUsingSearchEngine([FromQuery] SearchEngineQueries query)
    {
        return await Mediator.Send(query);
    }
}
