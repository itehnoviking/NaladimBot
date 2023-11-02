using NaladimBot.Data.Entities;

namespace NaladimBot.Data.Entities;
public class User : BaseEntities
{
    public long UserId { get; set; }
    public bool IsAdmin { get; set; }
}