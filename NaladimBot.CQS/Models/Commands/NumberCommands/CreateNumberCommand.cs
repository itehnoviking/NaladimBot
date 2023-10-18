using MediatR;
using NaladimBot.Core.DTOs;

namespace NaladimBot.CQS.Models.Commands.NumberCommands;

public class CreateNumberCommand : IRequest<bool>
{
    public CreateNumberCommand(NewNumberDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Mashine = dto.Mashine;
        StampPhoto = dto.StampPhoto;
        ReadyNumberPhoto = dto.ReadyNumberPhoto;
        TechnicalProcessPhoto = dto.TechnicalProcessPhoto;
        Comment = dto.Comment;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Mashine { get; set; }
    public byte[] StampPhoto { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string? Comment { get; set; }
}
