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
            PushL("Fill data of the new number");

            RowButton(state.NamesNumber == null ? $"Set or update name" : "Name ✅",
                Q(Fill_NameNumber));
            RowButton(string.IsNullOrEmpty(state.Mashine) ? "Set or update mashine" : "Mashine: " + state.Mashine,
                Q(Fill_PeekMashine, 0));
            RowButton(state.StampPhoto == null ? "Set or update stamp photo" : "Stamp Photo ✅",
                Q(Fill_StampPhoto));
            RowButton(state.ReadyNumberPhoto == null
                    ? "Set or update ready number photo"
                    : "Ready Number Photo ✅", Q(Fill_ReadyNumberPhoto));
            RowButton(state.TechnicalProcessPhoto == null
                    ? "Set or update technical process photo"
                    : "Technical Process Photo ✅", Q(Fill_TechnicalProcessPhoto));
            RowButton(string.IsNullOrEmpty(state.Comment) ? "Set or update comment" : "Comment: " + state.Comment,
                Q(Fill_Comment));

            if (state.IsSet)
            {
                RowButton("Schedule", Q(Fill, ""));
            }
        }

        [Action]
        async ValueTask Fill([State] FillStateNewNumber state)
        {
            await _numberService.CreateAsync(_mapper.Map<NewNumberDto>(state));

            PushL("✅ added");
        }


        [Action]
        async ValueTask Fill_PeekMashine(Mashine mashines)
        {
            Push("Peek mashine");

            new FlagMessageBuilder<Mashine>(mashines)
                .Navigation(s => Q(Fill_PeekMashine, s))
                .Build(Message);

            RowButton("Done ✅", Q(Fill_SetMashine, mashines, ""));
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
            Push("Add technical process photo this number");

            await State(new SetTechnicalProcessPhotoState());
        }

        [Action]
        async ValueTask Fill_ReadyNumberPhoto()
        {
            Push("Add ready number photo this number");

            await State(new SetReadyNumberPhotoState());
        }

        [Action]
        async ValueTask Fill_NameNumber()
        {
            Push("Enter the name of the new number");
            await State(new SetNameNumberState());
        }

        [Action]
        async ValueTask Fill_Comment()
        {
            await State(new SetCommentState());
            Push("Reply to this message with a comment");
        }

        [Action]
        async ValueTask Fill_StampPhoto()
        {
            await State(new SetStampPhotoState());
            Push("Add stamp photo this number");
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
            fillState = fillState with { NamesNumber = new List<NameDto>{ new NameDto(){NameNumber = Context.GetSafeTextPayload()} }};
            await AState(fillState);
            FillNewNumber(fillState);
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


        //private async Task<byte[]> GetImageBytesAsync(string fileId, IUpdateContext context)
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        //FileToSend file = await botClient.GetFileAsync(fileId);
        //        //await botClient.DownloadFileAsync(file.FilePath, stream);

        //        var file = await Context.Bot.Client.GetFileAsync(fileId);
        //        await Context.Bot.Client.DownloadFileAsync(file.FilePath, stream);

        //        return stream.ToArray();
        //    }
        //}
    }
}
