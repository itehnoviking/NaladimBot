using AutoMapper;
using NaladimBot.Core.DTOs;
using NaladimBot.CQS.Models.Commands.NumberCommands;
using NaladimBot.Data.Entities;
using NaladimBot.Models;

namespace NaladkaBot.Mappers;

public class NumberProfile : Profile
{
    public NumberProfile()
    {
        CreateMap<CreateNumberCommand, Number>();

        CreateMap<Number, NumberDto>();

        CreateMap<FillStateNewNumber, NewNumberDto>();

        CreateMap<NameDto, Name>()
            .ForMember(ent => ent.NameNumber, dto => dto.MapFrom(item => item.Name))
            .ReverseMap();
    }
}