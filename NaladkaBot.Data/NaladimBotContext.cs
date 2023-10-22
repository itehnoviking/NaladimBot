using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NaladimBot.Data.Entities;

namespace NaladimBot.Data;

public class NaladimBotContext : DbContext
{
    public NaladimBotContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Number> Numbers { get; set; }
    public DbSet<Name> Names { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));


    //}
}