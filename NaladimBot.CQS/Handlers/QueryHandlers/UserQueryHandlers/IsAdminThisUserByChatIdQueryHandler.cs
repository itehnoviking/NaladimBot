using MediatR;
using Microsoft.EntityFrameworkCore;
using NaladimBot.CQS.Models.Queries.UserQueries;
using NaladimBot.Data;
using Telegram.Bot.Types;

namespace NaladimBot.CQS.Handlers.QueryHandlers.UserQueryHandlers;

public class IsAdminThisUserByChatIdQueryHandler : IRequestHandler<IsAdminThisUserByUserIdQuery, bool>
{
    private readonly NaladimBotContext _database;

    public IsAdminThisUserByChatIdQueryHandler(NaladimBotContext database)
    {
        _database = database;
    }

    public async Task<bool> Handle(IsAdminThisUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _database.Users
            .AsNoTracking()
            .Where(a => a.UserId.Equals(request.UserId))
            .Select(a => a.IsAdmin)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

    }
}