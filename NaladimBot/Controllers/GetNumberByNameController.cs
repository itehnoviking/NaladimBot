using AutoMapper;
using Deployf.Botf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Domain.Services;
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
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public GetNumberByNameController(INumberService numberService, IMapper mapper, IImageService imageService, IUserService userService, IMemoryCache cache)
        {
            _numberService = numberService;
            _mapper = mapper;
            _imageService = imageService;
            _userService = userService;
            _cache = cache;
        }

        [Action("Найти номер по имени")]
        public async Task GetNumberByFragmentName()
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

                    ResponseMessagesByGetNumberName(isAdmin);
                }
            }

            else
            {
                ResponseMessagesByGetNumberName(isAdmin);
            }
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

            if (number.StampPhotoOne != null)
            {
                await _imageService.SendImageAsync(chatId, number.StampPhotoOne, Context);
            }
            if (number.StampPhotoTwo != null)
            {
                await _imageService.SendImageAsync(chatId, number.StampPhotoTwo, Context);
            }

            await _imageService.SendImageAsync(chatId, number.ReadyNumberPhoto, Context);

            if (number.Comment != null)
            {
                await Client.SendTextMessageAsync(chatId, number.Comment);
            }
        }

        record SetNameNumberState;

        private async void ResponseMessagesByGetNumberName(bool? isAdmin)
        {
            if (isAdmin == true)
            {
                await Fill_NameNumber();
            }

            else
            {
                PushL("You shall not pass!!");
            }
        }
    }
}
