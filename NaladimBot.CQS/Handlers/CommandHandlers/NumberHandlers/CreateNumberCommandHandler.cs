using AutoMapper;
using MediatR;
using NaladimBot.CQS.Models.Commands.NumberCommands;
using NaladimBot.Data;
using NaladimBot.Data.Entities;

namespace NaladimBot.CQS.Handlers.CommandHandlers.NumberHandlers;

public class CreateNumberCommandHandler : IRequestHandler<CreateNumberCommand, bool>
{
    private readonly NaladimBotContext _database;
    private readonly IMapper _mapper;

    public CreateNumberCommandHandler(NaladimBotContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateNumberCommand request, CancellationToken cancellationToken)
    {
        var number = _mapper.Map<Number>(request);

        await _database.Numbers
            .AddAsync(number);

        await _database.SaveChangesAsync(cancellationToken: cancellationToken);

        return true;
    }
}