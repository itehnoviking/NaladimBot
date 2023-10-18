using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;

namespace NaladimBot.Core.Interfaces.Services;

public interface IImageService
{
    Task<byte[]> GetImageBytesAsync(string fileId, IUpdateContext context);
    Task SendImageAsync(ChatId chatId, byte[] imageBytes, IUpdateContext context);
}