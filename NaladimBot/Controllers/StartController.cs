using Deployf.Botf;
using NaladimBot.Core.Interfaces.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace NaladimBot.Controllers;

public class StartController : BotController
{
    private readonly IUserService _userService;

    public StartController(IUserService userService)
    {
        _userService = userService;
    }

    [Action("/start")]
    public async Task Start()
    {
        var isAdmin = await _userService.IsAdminThisUserByChatIdAsync(Context.Update.Message.Chat.Id);

        if (isAdmin == true)
        {
            PushL("Hello!");
            PushL("This bot allow you adding a recurring reminder to chat");
            PushL();

            RowKButton("/add");
        }

        else
        {
            PushL("You shall not pass!!");
        }


        //RowButton("Найти номер", "/add");
    }

}