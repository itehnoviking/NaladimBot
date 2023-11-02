namespace NaladimBot.Data.Entities;

public class Number : BaseEntities
{
    public string Mashine { get; set; }
    public byte[]? StampPhotoOne { get; set; }
    public byte[]? StampPhotoTwo { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string? Comment { get; set; }

    public virtual IEnumerable<Name> Names { get; set; }
}