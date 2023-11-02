using Deployf.Botf;
using NaladimBot.Core.Interfaces.Services;
using System.IO;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace NaladimBot.Controllers;

public class StartController : BotController
{
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;

    public StartController(IUserService userService, IMemoryCache cache)
    {
        _userService = userService;
        _cache = cache;
    }

    [Action("/start")]
    public async Task Start()
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

                ResponseMessagesByStart(isAdmin);
            }
        }

        else
        {
            ResponseMessagesByStart(isAdmin);
        }
    }

    private void ResponseMessagesByStart(bool? isAdmin)
    {
        if (isAdmin == true)
        {
            PushL("Hello!");
            PushL();

            RowKButton("Добавить номер");
            RowKButton("Найти номер по имени");
            RowKButton("Найти номер по фрагменту имени");
        }

        else
        {
            PushL("You shall not pass!!");
        }
    }

}