using MediatR;
using NaladimBot.Core.DTOs;
using NaladimBot.Data.Entities;

namespace NaladimBot.CQS.Models.Commands.NumberCommands;

public class CreateNumberCommand : IRequest<bool>
{
    public CreateNumberCommand(NewNumberDto dto)
    {
        Id = dto.Id;
        NamesNumber = dto.NamesNumber;
        Mashine = dto.Mashine;
        StampPhoto = dto.StampPhoto;
        ReadyNumberPhoto = dto.ReadyNumberPhoto;
        TechnicalProcessPhoto = dto.TechnicalProcessPhoto;
        Comment = dto.Comment;
    }

    public Guid Id { get; set; }
    public List<NameDto> NamesNumber { get; set; }
    public string Mashine { get; set; }
    public byte[] StampPhoto { get; set; }
    public byte[] ReadyNumberPhoto { get; set; }
    public byte[] TechnicalProcessPhoto { get; set; }
    public string? Comment { get; set; }
}
