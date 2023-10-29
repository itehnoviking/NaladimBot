using Deployf.Botf;
using Microsoft.EntityFrameworkCore;
using NaladimBot.Core.Interfaces.Services;
using NaladimBot.Data;
using System.Reflection;
using MediatR;
using NaladimBot.Domain.Services;

BotfProgram.StartBot(args, onConfigure: (svc, cfg) =>
{
    var connectionString = cfg.GetConnectionString("DefaultConnection");

    svc.AddDbContext<NaladimBotContext>(opt
        => opt.UseNpgsql(connectionString));


    svc.AddScoped<INumberService, NumberService>();
    svc.AddScoped<IUserService, UserService>();
    svc.AddScoped<IImageService, ImageService>();

    //AutoMapper
    svc.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    Assembly.Load("NaladimBot.Cqs");
    svc.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

}, onRun: (app, cfg) =>
{
    app.UseHttpsRedirection();
    app.UseHsts();
});
