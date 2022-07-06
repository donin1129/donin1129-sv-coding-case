using MediatR;
using SvCodingCase.Application.Common.Models;
using SvCodingCase.DTOs;

namespace SvCodingCase.Application.Queries;


/// <summary>
///     Maps the request parameters necessary to fetch tasks by task id
/// </summary>
public class GetBuildings : IRequest<PaginatedList<BuildingDto>>
{
    /// <summary>
    ///     create instance
    /// </summary>
    /// <param name="buildingIds">the building ids</param>
    /// <param name="names">the building names</param>
    /// <param name="shortCuts">the building shortCuts</param>
    public GetBuildings(IReadOnlyList<Guid>? buildingIds, IReadOnlyList<string>? names, IReadOnlyList<string>? shortCuts, int pageNumber = 0, int pageSize = 25)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        BuildingIds = buildingIds;
        Names = names;
        ShortCuts = shortCuts;
    }
        
    /// <summary>
    ///     The query parameter to find building ids
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    ///     The query parameter to find building ids
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    ///     The query parameter to find building ids
    /// </summary>
    public IReadOnlyList<Guid>? BuildingIds { get; }

    /// <summary>
    ///     The query parameter to find building names 
    /// </summary>
    public IReadOnlyList<string>? Names { get; }

    /// <summary>
    ///     The query parameter to find building shortCuts 
    /// </summary>
    public IReadOnlyList<string>? ShortCuts { get; }
}
