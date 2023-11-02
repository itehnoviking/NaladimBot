namespace NaladimBot.Core.Interfaces.Services;

public interface IUserService
{
    Task<long> GetUserChatIdAsync();
    Task<bool> IsAdminThisUserByUserIdAsync(long chatId);
}