using Deployf.Botf;
using AutoMapper;
using NaladimBot.Core.DTOs;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Models;
using Telegram.Bot;
using Telegram.Bot.Framework.Abstractions;
using NaladimBot.Domain.Services;
using Microsoft.Extensions.Caching.Memory;

namespace NaladimBot.Controllers
{
    public class AddNumberController : BotController
    {
        private readonly INumberService _numberService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public AddNumberController(INumberService numberService, IMapper mapper, IImageService imageService, IUserService userService, IMemoryCache cache)
        {
            _numberService = numberService;
            _mapper = mapper;
            _imageService = imageService;
            _userService = userService;
            _cache = cache;
        }


        [Action("Добавить номер", "add a number")]
        public async Task Add()
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

                    await ResponseMessagesByAddNumberAsync(isAdmin);
                }
            }

            else
            {
                await ResponseMessagesByAddNumberAsync(isAdmin);
            }
        }

        [Action]
        private async Task FillNewNumber([State] FillStateNewNumber state)
        {
            PushL("Заполните данные номера");

            RowButton(state.Names == null ? $"Имя номера" : "Имя номера ✅",
                Q(Fill_NameNumber));
            RowButton(string.IsNullOrEmpty(state.Mashine) ? "Оборудование" : "Оборудование: " + state.Mashine,
                Q(Fill_PeekMashine, 0));
            RowButton(state.StampPhotoOne == null ? "Фото штампа" : "Фото штампа ✅",
                Q(Fill_StampPhotoOne));
            RowButton(state.StampPhotoTwo == null ? "Фото штампа" : "Фото штампа ✅",
                Q(Fill_StampPhotoTwo));
            RowButton(state.ReadyNumberPhoto == null
                    ? "Фото готовой детали"
                    : "Фото готовой детали ✅", Q(Fill_ReadyNumberPhoto));
            RowButton(state.TechnicalProcessPhoto == null
                    ? "Фото технического процесса"
                    : "Фото технического процесса ✅", Q(Fill_TechnicalProcessPhoto));
            RowButton(string.IsNullOrEmpty(state.Comment) ? "Комментарий" : "Комментарий: " + state.Comment,
                Q(Fill_Comment));

            if (state.IsSet)
            {
                RowButton("Сохранить", Q(Fill, ""));
            }
        }

        [Action]
        private async Task Fill([State] FillStateNewNumber state)
        {
            await _numberService.CreateAsync(_mapper.Map<NewNumberDto>(state));

            PushL("Добавлено ✅");
        }


        [Action]
        private async Task Fill_PeekMashine(Mashine mashines)
        {
            Push("Выберете оборудование");

            new FlagMessageBuilder<Mashine>(mashines)
                .Navigation(s => Q(Fill_PeekMashine, s))
                .Build(Message);

            RowButton("Выбрать ✅", Q(Fill_SetMashine, mashines, ""));
        }

        [Action]
        private async Task Fill_SetMashine(Mashine mashines, [State] FillStateNewNumber state)
        {
            state = state with { Mashine = mashines.ToString() };
            await AState(state);
            await FillNewNumber(state);
        }

        [Action]
        private async Task Fill_TechnicalProcessPhoto()
        {
            Push("Добавьте фото технического процесса");

            await State(new SetTechnicalProcessPhotoState());
        }

        [Action]
        private async Task Fill_ReadyNumberPhoto()
        {
            Push("Добавьте фото готовой детали");

            await State(new SetReadyNumberPhotoState());
        }

        [Action]
        private async Task Fill_NameNumber()
        {
            Push("Введите имя номера");
            await State(new SetNameNumberState());
        }

        [Action]
        private async Task Fill_Comment()
        {
            await State(new SetCommentState());
            Push("Напишите комментарий по поводу этого номера");
        }

        [Action]
        private async Task Fill_StampPhotoOne()
        {
            await State(new SetStampPhotoOneState());
            Push("Добавьте фото штампа");
        }

        [Action]
        private async Task Fill_StampPhotoTwo()
        {
            await State(new SetStampPhotoTwoState());
            Push("Добавьте фото штампа");
        }

        [State]
        private async Task Fill_StateTechnicalProcessPhoto(SetTechnicalProcessPhotoState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with
            {
                TechnicalProcessPhoto = await _imageService
                    .GetImageBytesAsync(Context.Update.Message.Photo[3].FileId, Context)
            };
            await AState(fillState);
            await FillNewNumber(fillState);
        }

        [State]
        private async Task Fill_StateReadyNumberPhoto(SetReadyNumberPhotoState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with
            {
                ReadyNumberPhoto = await _imageService.GetImageBytesAsync(Context.Update.Message.Photo[3].FileId, Context)

            };
            await AState(fillState);
            await FillNewNumber(fillState);
        }

        [State]
        private async Task Fill_StateStampPhotoOne(SetStampPhotoOneState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with
            {
                StampPhotoOne = await _imageService.GetImageBytesAsync(Context.Update.Message.Photo[3].FileId, Context)

            };
            await AState(fillState);
            await FillNewNumber(fillState);
        }

        [State]
        private async Task Fill_StateStampPhotoTwo(SetStampPhotoTwoState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with
            {
                StampPhotoTwo = await _imageService.GetImageBytesAsync(Context.Update.Message.Photo[3].FileId, Context)

            };
            await AState(fillState);
            await FillNewNumber(fillState);
        }

        [State]
        private async Task Fill_StateNameNumber(SetNameNumberState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();

            if (!Context.Update.Message.Text.Contains(','))
            {
                fillState = fillState with { Names = new List<NameDto> { new NameDto() { Name = Context.GetSafeTextPayload() } } };
            }

            else
            {
                var listNames = await _numberService.GetListNamesFromStr(Context.Update.Message.Text);

                fillState.Names = new List<NameDto>();

                foreach (var name in listNames)
                {
                    fillState.Names.Add(new NameDto() { Name = name });
                }
            }

            await AState(fillState);
            await FillNewNumber(fillState);
        }



        [State]
        private async Task Fill_StateComment(SetCommentState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with { Comment = Context.GetSafeTextPayload() };
            await AState(fillState);
            await FillNewNumber(fillState);
        }



        record SetCommentState;
        record SetNameNumberState;
        record SetStampPhotoOneState;
        record SetStampPhotoTwoState;
        record SetReadyNumberPhotoState;
        record SetTechnicalProcessPhotoState;

        private async Task ResponseMessagesByAddNumberAsync(bool? isAdmin)
        {
            if (isAdmin == true)
            {
                await FillNewNumber(await GetAState<FillStateNewNumber>());
            }

            else
            {

                PushL("You shall not pass!!");
            }
        }
    }
}
