namespace NaladimBot.Models;

public struct FillState
{
    public string Name { get; set; }
    public string Mashine { get; set; }
    public byte[] StampPhoto { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string Comment { get; set; }

    public bool IsSet => !string.IsNullOrEmpty(Name)
                         && !string.IsNullOrEmpty(Mashine)
                         && StampPhoto != null
                         && ReadyNumberPhoto != null
                         && TechnicalProcessPhoto != null
                         && !string.IsNullOrEmpty(Comment);

}