using MediatR;

namespace NaladimBot.CQS.Models.Queries.NumberQueries;

public class GetAllNumberNameByFragmentNameQuery : IRequest<IList<string>>
{
    public GetAllNumberNameByFragmentNameQuery(string fragmentName)
    {
        FragmentName = fragmentName;
    }

    public string FragmentName { get; set; }
}
