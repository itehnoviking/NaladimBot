using AutoMapper;
using MediatR;
using NaladimBot.Core.DTOs;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.CQS.Models.Commands.NumberCommands;
using NaladimBot.CQS.Models.Queries.NumberQueries;

namespace NaladimBot.Domain.Services;

public class NumberService : INumberService
{
    //private readonly ILogger<NumberService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public NumberService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IList<string>> GetAllNumberNamesAsync()
    {
        return await _mediator.Send(new GetAllNumberNamesQuery(), new CancellationToken());
    }

    public async Task CreateAsync(NewNumberDto dto)
    {
        try
        {
            var command = new CreateNumberCommand(dto);

            await _mediator.Send(command, new CancellationToken());
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, ex.Message);
            throw;
        }
        
    }

    public async Task<NumberDto> GetNumberByNameAsync(string name)
    {
        var dto = await _mediator.Send(new GetNumberByNameQuery(name), new CancellationToken());

        return dto;
    }

    public async Task<IList<string>> GetAllNumberNameByFragmentNameAsync(string fragmentName)
    {
        var list = await _mediator.Send(new GetAllNumberNameByFragmentNameQuery(fragmentName), new CancellationToken());

        return list;
    }

    public async Task<IList<string>> GetListNamesFromStr(string names)
    {
        var listNames = names.Split(',').ToList();

        return listNames;
    }
}