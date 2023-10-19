using AutoMapper;
using Deployf.Botf;
using NaladimBot.Core.DTOs;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Models;
using System.Reflection.PortableExecutable;

namespace NaladimBot.Controllers;

public class GetNumberByFragmentNameController : BotController
{
    private readonly INumberService _numberService;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public GetNumberByFragmentNameController(INumberService numberService, IMapper mapper, IImageService imageService)
    {
        _numberService = numberService;
        _mapper = mapper;
        _imageService = imageService;
    }

    [Action("Найти номер по фрагменту имени")]
    public async Task GetNumberListByFragmentName()
    {
        await Fill_FragmentNameNumber();


    }

    [Action]
    async ValueTask Fill_FragmentNameNumber()
    {
        PushL("Введите фрагмент имени искомого номера!");

        await State(new SetFragmentNameNumberState());
    }

    [Action]
    async ValueTask Fill([State] FillStateGetNumberByFragmentName state)
    {
        var list = await _numberService.GetAllNumberNameByFragmentNameAsync(state.FragmentName);
        PushL($"Найден {list.Count}!");

        foreach (var element in list)
        {
            PushL(element);
        }
    }

    [State]
    async Task Fill_FragmentName(SetFragmentNameNumberState state)
    {
        var fillState = await GetAState<FillStateGetNumberByFragmentName>();
        fillState = fillState with { FragmentName = Context.Update.Message.Text };

        await Fill(fillState);
    }

    record SetFragmentNameNumberState;
}