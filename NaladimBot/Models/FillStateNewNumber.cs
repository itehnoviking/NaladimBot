using NaladimBot.Core.DTOs;

namespace NaladimBot.Models;

public struct FillStateNewNumber
{
    public List<NameDto> NamesNumber { get; set; }
    public string Mashine { get; set; }
    public byte[] StampPhoto { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string Comment { get; set; }

    public bool IsSet => NamesNumber != null
                         && !string.IsNullOrEmpty(Mashine)
                         && StampPhoto != null
                         && ReadyNumberPhoto != null
                         && TechnicalProcessPhoto != null
                         && !string.IsNullOrEmpty(Comment);

}