using NaladimBot.Core.DTOs;

namespace NaladimBot.Core.Interfaces.Services;

public interface INumberService
{
    Task<IList<string>> GetAllNumberNamesAsync();
    Task CreateAsync(NewNumberDto dto);
    Task<NumberDto> GetNumberByNameAsync(string name);
    Task<IList<string>> GetAllNumberNameByFragmentNameAsync(string fragmentName);
}