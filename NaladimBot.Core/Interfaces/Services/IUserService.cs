namespace NaladimBot.Core.Interfaces.Services;

public interface IUserService
{
    Task<long> GetUserChatIdAsync();
    Task<bool> IsAdminThisUserByChatIdAsync(long chatId);
}