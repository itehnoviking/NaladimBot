namespace NaladimBot.Data.Entities;

public class Name : BaseEntities
{
    public string NameNumber { get; set; }

    public Guid NumberId { get; set; }
    public virtual Number Number { get; set; }
}