using MediatR;

namespace NaladimBot.CQS.Models.Queries.UserQueries;

public class IsAdminThisUserByUserIdQuery : IRequest<bool>
{
    public IsAdminThisUserByUserIdQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; set; }
}