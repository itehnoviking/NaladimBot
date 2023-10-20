using MediatR;
using Microsoft.EntityFrameworkCore;
using NaladimBot.CQS.Models.Queries.NumberQueries;
using NaladimBot.Data;

namespace NaladimBot.CQS.Handlers.QueryHandlers.NumberQueryHandlers;

public class GetAllNumberNamesQueryHandler : IRequestHandler<GetAllNumberNamesQuery, IList<string>>
{
    private readonly NaladimBotContext _database;

    public GetAllNumberNamesQueryHandler(NaladimBotContext database)
    {
        _database = database;
    }

    public async Task<IList<string>> Handle(GetAllNumberNamesQuery request, CancellationToken cancellationToken)
    {
        var numbersName = await _database.Names
            .AsNoTracking()
            .Select(a => a.NameNumber)
            .ToArrayAsync(cancellationToken: cancellationToken);
        

        return numbersName;
    }
}