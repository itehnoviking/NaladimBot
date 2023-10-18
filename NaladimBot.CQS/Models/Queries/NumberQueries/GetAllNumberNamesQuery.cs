using MediatR;

namespace NaladimBot.CQS.Models.Queries.NumberQueries;

public class GetAllNumberNamesQuery : IRequest<IList<string>>
{
    
}