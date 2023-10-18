using MediatR;

namespace NaladimBot.CQS.Models.Queries.UserQueries;

public class IsAdminThisUserByChatIdQuery : IRequest<bool>
{
    public IsAdminThisUserByChatIdQuery(long chatId)
    {
        ChatId = chatId;
    }

    public long ChatId { get; set; }
}