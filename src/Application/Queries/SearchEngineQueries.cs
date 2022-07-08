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

        // TODO-ZD: Implement linq translatable searches to improve performance. 

        IQueryable<Building> buildingQuery = _context.Buildings;
        IQueryable<Group> groupQuery = _context.Groups;
        IQueryable<Lock> lockQuery = _context.Locks;
        IQueryable<Media> mediaQuery = _context.Medias;

        if (request.SearchString == null || request.SearchString == "")
        {

            var buildingListEmptySearch = buildingQuery
                .Select(b => _mapper.Map<BuildingDto>(b))
                .ToList();

            var lockListEmptySearch = lockQuery
                .Select(b => _mapper.Map<LockDto>(b))
                .ToList();

            var groupListEmptySearch = groupQuery
                .Select(g => _mapper.Map<GroupDto>(g))
                .ToList();

            var mediaListEmptySearch = mediaQuery
                .Select(m => _mapper.Map<MediaDto>(m))
                .ToList();

            var resultEmptySearch = new DataFileDto()
            {
                Buildings = buildingListEmptySearch,
                Groups = groupListEmptySearch,
                Locks = lockListEmptySearch,
                Media = mediaListEmptySearch
            };

            return resultEmptySearch;
        } 

        var buildingList = buildingQuery
            .AsEnumerable()
            .OrderByDescending(b => b.CalcWeight(request.SearchString))
            .Select(b => _mapper.Map<BuildingDto>(b))
            .ToList();

        var lockList = lockQuery
            .AsEnumerable()
            .OrderByDescending(b => b.CalcWeight(request.SearchString))
            .Select(b => _mapper.Map<LockDto>(b))
            .ToList();

        var groupList = groupQuery
            .AsEnumerable()
            .OrderByDescending(g => g.CalcWeight(request.SearchString))
            .Select(g => _mapper.Map<GroupDto>(g))
            .ToList();

        var mediaList = mediaQuery
            .AsEnumerable()
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
