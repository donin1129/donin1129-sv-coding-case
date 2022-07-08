using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.DTOs;
using SvCodingCase.Domain.Entities;
using AutoMapper;
using MediatR;

namespace SvCodingCase.Application.Queries;

public record SearchEngineQueries : IRequest<DataFileDto>
{
    public string SearchString { get; init; } = "";
}

public class SearchEngineQueriesHandler : IRequestHandler<SearchEngineQueries, DataFileDto>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public SearchEngineQueriesHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DataFileDto> Handle(SearchEngineQueries request, CancellationToken cancellationToken)
    {
        IQueryable<Building> buildingQuery = _context.Buildings;
        IQueryable<Group> groupQuery = _context.Groups;
        IQueryable<Lock> lockQuery = _context.Locks;
        IQueryable<Media> mediaQuery = _context.Medias;

        var buildingList = buildingQuery
            .OrderByDescending(b => b.CalcWeight(request.SearchString))
            .Select(b => _mapper.Map<BuildingDto>(b))
            .ToList();

        var lockList = lockQuery
            .OrderByDescending(l => l.CalcWeight(request.SearchString))
            .Select(l => _mapper.Map<LockDto>(l))
            .ToList();

        var groupList = groupQuery
            .OrderByDescending(g => g.CalcWeight(request.SearchString))
            .Select(g => _mapper.Map<GroupDto>(g))
            .ToList();

        var mediaList = mediaQuery
            .OrderByDescending(m => m.CalcWeight(request.SearchString))
            .Select(m => _mapper.Map<MediaDto>(m))
            .ToList();

        var result = new DataFileDto()
        {
            Buildings = buildingList,
            Groups = groupList,
            Locks = lockList,
            Media = mediaList
        };

        return result;
    }
}
