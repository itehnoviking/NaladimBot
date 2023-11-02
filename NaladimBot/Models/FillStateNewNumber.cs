using NaladimBot.Core.DTOs;

namespace NaladimBot.Models;

public struct FillStateNewNumber
{
    public List<NameDto> Names { get; set; }
    public string Mashine { get; set; }
    public byte[]? StampPhotoOne { get; set; }
    public byte[]? StampPhotoTwo { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string? Comment { get; set; }

    public bool IsSet => Names != null
                         && !string.IsNullOrEmpty(Mashine)
                         && ReadyNumberPhoto != null
                         && TechnicalProcessPhoto != null;

}