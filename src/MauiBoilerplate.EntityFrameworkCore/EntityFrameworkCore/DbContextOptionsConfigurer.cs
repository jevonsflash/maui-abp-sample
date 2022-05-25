using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MauiBoilerplate.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<MauiBoilerplateDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for MauiBoilerplateDbContext */
            dbContextOptions.UseSqlite(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MauiBoilerplateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlite(connection);
        }
    }
}
