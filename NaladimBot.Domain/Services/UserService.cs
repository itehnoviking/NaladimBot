using AutoMapper;
using MediatR;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.CQS.Models.Queries.UserQueries;

namespace NaladimBot.Domain.Services;

public class UserService : IUserService
{
    //private readonly ILogger<UserService> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    
    public async Task<long> GetUserChatIdAsync()
    {
        return await _mediator.Send(new GetUserChatIdQuery(), new CancellationToken());
    }

    public async Task<bool> IsAdminThisUserByChatIdAsync(long chatId)
    {
        return await _mediator.Send(new IsAdminThisUserByChatIdQuery(chatId), new CancellationToken());
    }
}