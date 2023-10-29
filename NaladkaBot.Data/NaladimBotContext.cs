using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NaladimBot.Data.Entities;

namespace NaladimBot.Data;

public class NaladimBotContext : DbContext
{
    private readonly IConfiguration _configuration;

    public NaladimBotContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        Database.EnsureCreated();

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Number> Numbers { get; set; }
    public DbSet<Name> Names { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));


    //}
}