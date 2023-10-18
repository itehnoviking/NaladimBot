﻿namespace NaladimBot.Data.Entities;

public class Number : BaseEntities
{
    public string Name { get; set; }
    public string? Mashine { get; set; }
    public byte[]? StampPhoto { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string? Comment { get; set; }
}