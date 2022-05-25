using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MauiBoilerplate.Core.Entities;

namespace MauiBoilerplate.EntityFrameworkCore
{
    public class MauiBoilerplateDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public DbSet<Song> Song { get; set; }
        public MauiBoilerplateDbContext(DbContextOptions<MauiBoilerplateDbContext> options) 
            : base(options)
        {

        }
    }
}
