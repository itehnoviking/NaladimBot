using Deployf.Botf;
using AutoMapper;
using NaladimBot.Core.DTOs;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Models;
using Telegram.Bot;
using Telegram.Bot.Framework.Abstractions;

namespace NaladimBot.Controllers
{
    public class AddNumberController : BotController
    {
        private readonly INumberService _numberService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public AddNumberController(INumberService numberService, IMapper mapper, IImageService imageService)
        {
            _numberService = numberService;
            _mapper = mapper;
            _imageService = imageService;
        }


        [Action("Добавить номер", "add a number")]
        public async ValueTask Add()
        {
            await FillNewNumber(await GetAState<FillStateNewNumber>());
        }

        [Action]
        async ValueTask FillNewNumber([State] FillStateNewNumber state)
        {
            PushL("Заполните данные номера");

            RowButton(state.Names == null ? $"Имя номера" : "Имя номера ✅",
                Q(Fill_NameNumber));
            RowButton(string.IsNullOrEmpty(state.Mashine) ? "Оборудование" : "Оборудование: " + state.Mashine,
                Q(Fill_PeekMashine, 0));
            RowButton(state.StampPhoto == null ? "Фото штампа" : "Фото штампа ✅",
                Q(Fill_StampPhoto));
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
        async ValueTask Fill([State] FillStateNewNumber state)
        {
            await _numberService.CreateAsync(_mapper.Map<NewNumberDto>(state));

            PushL("Добавлено ✅");
        }


        [Action]
        async ValueTask Fill_PeekMashine(Mashine mashines)
        {
            Push("Выберете оборудование");

            new FlagMessageBuilder<Mashine>(mashines)
                .Navigation(s => Q(Fill_PeekMashine, s))
                .Build(Message);

            RowButton("Выбрать ✅", Q(Fill_SetMashine, mashines, ""));
        }

        [Action]
        async ValueTask Fill_SetMashine(Mashine mashines, [State] FillStateNewNumber state)
        {
            state = state with { Mashine = mashines.ToString() };
            await AState(state);
            await FillNewNumber(state);
        }

        [Action]
        async ValueTask Fill_TechnicalProcessPhoto()
        {
            Push("Добавьте фото технического процесса");

            await State(new SetTechnicalProcessPhotoState());
        }

        [Action]
        async ValueTask Fill_ReadyNumberPhoto()
        {
            Push("Добавьте фото готовой детали");

            await State(new SetReadyNumberPhotoState());
        }

        [Action]
        async ValueTask Fill_NameNumber()
        {
            Push("Введите имя номера");
            await State(new SetNameNumberState());
        }

        [Action]
        async ValueTask Fill_Comment()
        {
            await State(new SetCommentState());
            Push("Напишите комментарий по поводу этого номера");
        }

        [Action]
        async ValueTask Fill_StampPhoto()
        {
            await State(new SetStampPhotoState());
            Push("Добавьте фото штампа");
        }

        [State]
        async ValueTask Fill_StateTechnicalProcessPhoto(SetTechnicalProcessPhotoState state)
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
        async ValueTask Fill_StateReadyNumberPhoto(SetReadyNumberPhotoState state)
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
        async ValueTask Fill_StateStampPhoto(SetStampPhotoState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with
            {
                StampPhoto = await _imageService.GetImageBytesAsync(Context.Update.Message.Photo[3].FileId, Context)

            };
            await AState(fillState);
            await FillNewNumber(fillState);
        }

        [State]
        async ValueTask Fill_StateNameNumber(SetNameNumberState state)
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
                    fillState.Names.Add(new NameDto(){Name = name });
                }
            }
            
            await AState(fillState);
            await FillNewNumber(fillState);
        }



        [State]
        async ValueTask Fill_StateComment(SetCommentState state)
        {
            var fillState = await GetAState<FillStateNewNumber>();
            fillState = fillState with { Comment = Context.GetSafeTextPayload() };
            await AState(fillState);
            await FillNewNumber(fillState);
        }



        record SetCommentState;
        record SetNameNumberState;
        record SetStampPhotoState;
        record SetReadyNumberPhotoState;
        record SetTechnicalProcessPhotoState;
    }
}
