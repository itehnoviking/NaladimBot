using MediatR;
using Microsoft.EntityFrameworkCore;
using NaladimBot.CQS.Models.Queries.NumberQueries;
using NaladimBot.Data;

namespace NaladimBot.CQS.Handlers.QueryHandlers.NumberQueryHandlers;

public class GetAllNumberNameByFragmentNameQueryHandler : IRequestHandler<GetAllNumberNameByFragmentNameQuery, IList<string>>
{
    private readonly NaladimBotContext _database;

    public GetAllNumberNameByFragmentNameQueryHandler(NaladimBotContext database)
    {
        _database = database;
    }

    public async Task<IList<string>> Handle(GetAllNumberNameByFragmentNameQuery request, CancellationToken cancellationToken)
    {
        var nameList = await _database.Numbers
            .AsNoTracking()
            .Select(a => a.Name)
            .Where(a => a.Contains(request.FragmentName))
            .ToArrayAsync(cancellationToken: cancellationToken);

        return nameList;
    }
}