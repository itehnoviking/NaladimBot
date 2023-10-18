using NaladimBot.Core.Interfaces.Services;
using Telegram.Bot;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace NaladimBot.Domain.Services;

public class ImageService : IImageService
{

    public async Task<byte[]> GetImageBytesAsync(string fileId, IUpdateContext context)
    {
        using (var stream = new MemoryStream())
        {
            var file = await context.Bot.Client.GetFileAsync(fileId);
            await context.Bot.Client.DownloadFileAsync(file.FilePath, stream);

            return stream.ToArray();
        }
    }


    public async Task SendImageAsync(ChatId chatId, byte[] imageBytes, IUpdateContext context)
    {
        var rnd = new Random();

        using (var stream = new MemoryStream(imageBytes))
        {
            await context.Bot.Client.SendPhotoAsync(chatId, new InputOnlineFile(stream, $"image{rnd.Next()}.jpg"));
        }
    }
}

