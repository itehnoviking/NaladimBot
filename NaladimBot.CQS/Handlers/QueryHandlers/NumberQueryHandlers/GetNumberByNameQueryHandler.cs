using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NaladimBot.Core.DTOs;
using NaladimBot.CQS.Models.Queries.NumberQueries;
using NaladimBot.Data;

namespace NaladimBot.CQS.Handlers.QueryHandlers.NumberQueryHandlers;

public class GetNumberByNameQueryHandler : IRequestHandler<GetNumberByNameQuery, NumberDto>
{
    private readonly NaladimBotContext _database;
    private readonly IMapper _mapper;

    public GetNumberByNameQueryHandler(NaladimBotContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<NumberDto> Handle(GetNumberByNameQuery request, CancellationToken cancellationToken)
    {
        var number = await _database.Numbers
            .AsNoTracking()
            .Where(a => a.NamesNumber.Equals(request.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return _mapper.Map<NumberDto>(number);
    }
}