﻿using AutoMapper;
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

        CreateMap<FillState, NewNumberDto>();
    }
}