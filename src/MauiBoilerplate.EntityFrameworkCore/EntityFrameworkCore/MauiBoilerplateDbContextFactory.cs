using System;
using System.Diagnostics;
using System.IO;
using MauiBoilerplate.Core;
using MauiBoilerplate.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MauiBoilerplate.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class MauiBoilerplateDbContextFactory : IDesignTimeDbContextFactory<MauiBoilerplateDbContext>
    {
        public MauiBoilerplateDbContext CreateDbContext(string[] args)
        {
            var sqliteFilename = "mato.db";
            string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), sqliteFilename);
            var builder = new DbContextOptionsBuilder<MauiBoilerplateDbContext>();
            var hostFolder = Path.Combine(Environment.CurrentDirectory, "bin", "Debug", "net6.0");

            var configuration = AppConfigurations.Get(hostFolder);
            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(MauiBoilerplateConsts.ConnectionStringName)
            );

            return new MauiBoilerplateDbContext(builder.Options);

        }
    }
}