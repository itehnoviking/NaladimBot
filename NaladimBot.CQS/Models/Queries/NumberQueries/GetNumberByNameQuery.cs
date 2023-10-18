using MediatR;
using NaladimBot.Core.DTOs;

namespace NaladimBot.CQS.Models.Queries.NumberQueries;

public class GetNumberByNameQuery : IRequest<NumberDto>
{
    public GetNumberByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}