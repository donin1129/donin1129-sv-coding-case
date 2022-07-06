using AutoMapper;
using AutoMapper.QueryableExtensions;
using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SvCodingCase.Application.Common.Models;
using SvCodingCase.DTOs;
using System.Linq.Expressions;
using SvCodingCase.Domain.Entities;
using SvCodingCase.Application.Common.Mappings;

namespace SvCodingCase.Application.Queries;

/*[Authorize]
public record GetBuildingsQuery : IRequest<GetBuildings>;*/

public class GetBuildingsHandler : IRequestHandler<GetBuildings, PaginatedList<BuildingDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetBuildingsHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BuildingDto>> Handle(GetBuildings request, CancellationToken cancellationToken)
    {
        IQueryable<Building> filtered = _context.Buildings;

        if (request.Names is not null)
        {
            filtered = filtered.Where(building => request.Names.Contains(building.Name));
        }

        if (request.ShortCuts is not null)
        {
            filtered = filtered.Where(building => request.ShortCuts.Contains(building.ShortCut));
        }

        filtered = filtered.Include(building => building.Locks);

        var mapped = filtered.Select(building => _mapper.Map<BuildingDto>(building));

        return await mapped.PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
