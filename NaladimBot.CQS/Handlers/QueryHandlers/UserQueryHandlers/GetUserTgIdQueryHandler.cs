using MediatR;
using Microsoft.EntityFrameworkCore;
using NaladimBot.CQS.Models.Queries.UserQueries;
using NaladimBot.Data;

namespace NaladimBot.CQS.Handlers.QueryHandlers.UserQueryHandlers;

public class GetUserChatIdQueryHandler : IRequestHandler<GetUserUserIdQuery, long>
{
    private readonly NaladimBotContext _database;

    public GetUserChatIdQueryHandler(NaladimBotContext database)
    {
        _database = database;
    }

    public async Task<long> Handle(GetUserUserIdQuery request, CancellationToken cancellationToken)
    {
        var chatId = await _database.Users
            .AsNoTracking()
            .Select(u => u.UserId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return chatId;

    }
}