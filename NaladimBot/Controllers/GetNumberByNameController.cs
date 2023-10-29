using AutoMapper;
using Deployf.Botf;
using Microsoft.AspNetCore.Mvc;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NaladimBot.Controllers
{
    public class GetNumberByNameController : BotController
    {
        private readonly INumberService _numberService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public GetNumberByNameController(INumberService numberService, IMapper mapper, IImageService imageService)
        {
            _numberService = numberService;
            _mapper = mapper;
            _imageService = imageService;
        }

        [Action("Найти номер по имени")]
        public async Task GetNumberByFragmentName()
        {
            await Fill_NameNumber();


        }

        [Action]
        async ValueTask Fill_NameNumber()
        {
            PushL("Введите имя искомого номера!");

            await State(new SetNameNumberState());
        }

        [State]
        async Task Fill_FragmentName(SetNameNumberState state)
        {
            var fillState = await GetAState<FillStateGetNumberByName>();
            fillState = fillState with { Name = Context.Update.Message.Text };

            await Fill(fillState);
        }

        [Action]
        async ValueTask Fill([State] FillStateGetNumberByName state)
        {
            var chatId = Context.Update.Message.Chat.Id;
            var number = await _numberService.GetNumberByNameAsync(state.Name);

            foreach (var nameDto in number.Names)
            {
                await Client.SendTextMessageAsync(chatId, nameDto.Name);
            }
            
            await Client.SendTextMessageAsync(chatId, number.Mashine);
            await _imageService.SendImageAsync(chatId, number.TechnicalProcessPhoto, Context);

            if (number.StampPhoto.Any())
            {
                await _imageService.SendImageAsync(chatId, number.StampPhoto, Context);
            }
            
            await _imageService.SendImageAsync(chatId, number.ReadyNumberPhoto, Context);
            await Client.SendTextMessageAsync(chatId, number.Comment);
        }

        record SetNameNumberState;
    }
}
