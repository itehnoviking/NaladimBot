using AutoMapper;
using Deployf.Botf;
using Microsoft.Extensions.Caching.Memory;
using NaladimBot.Core.DTOs;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Domain.Services;
using NaladimBot.Models;
using System.Reflection.PortableExecutable;

namespace NaladimBot.Controllers;

public class GetNumberByFragmentNameController : BotController
{
    private readonly INumberService _numberService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;

    public GetNumberByFragmentNameController(INumberService numberService, IMapper mapper, IUserService userService, IMemoryCache cache)
    {
        _numberService = numberService;
        _mapper = mapper;
        _userService = userService;
        _cache = cache;
    }

    [Action("Найти номер по фрагменту имени")]
    public async Task GetNumberListByFragmentName()
    {
        var userId = Context.Update.Message.From.Id;

        _cache.TryGetValue(userId, out bool? isAdmin);

        if (isAdmin == null)
        {
            isAdmin = await _userService.IsAdminThisUserByUserIdAsync(userId);

            if (isAdmin != null)
            {
                _cache.Set(userId, isAdmin,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

                await ResponseMessagesByGetNumberFragmentNameAsync(isAdmin);
            }
        }

        else
        {
            await ResponseMessagesByGetNumberFragmentNameAsync(isAdmin);
        }
    }

    [Action]
    private async Task Fill_FragmentNameNumber()
    {
        PushL("Введите фрагмент имени искомого номера!");

        await State(new SetFragmentNameNumberState());
    }

    [Action]
    private async Task Fill([State] FillStateGetNumberByFragmentName state)
    {
        var list = await _numberService.GetAllNumberNameByFragmentNameAsync(state.FragmentName);
        PushL($"Найден {list.Count}!");

        foreach (var element in list)
        {
            PushL(element);
        }
    }

    [State]
    private async Task Fill_FragmentName(SetFragmentNameNumberState state)
    {
        var fillState = await GetAState<FillStateGetNumberByFragmentName>();
        fillState = fillState with { FragmentName = Context.Update.Message.Text };

        await Fill(fillState);
    }

    record SetFragmentNameNumberState;

    private async Task ResponseMessagesByGetNumberFragmentNameAsync(bool? isAdmin)
    {
        if (isAdmin == true)
        {
            await Fill_FragmentNameNumber();
        }

        else
        {
            PushL("You shall not pass!!");
        }
    }
}