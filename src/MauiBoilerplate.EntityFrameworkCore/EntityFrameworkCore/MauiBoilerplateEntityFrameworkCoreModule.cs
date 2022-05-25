using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.EntityFrameworkCore.Uow;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MauiBoilerplate.Core;
using MauiBoilerplate.EntityFrameworkCore.Seed;
using Microsoft.EntityFrameworkCore;

namespace MauiBoilerplate.EntityFrameworkCore
{
    [DependsOn(
        typeof(MauiBoilerplateCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class MauiBoilerplateEntityFrameworkCoreModule : AbpModule
    {
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<MauiBoilerplateDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }
        public override void Initialize()
        {
 
            IocManager.RegisterAssemblyByConvention(typeof(MauiBoilerplateEntityFrameworkCoreModule).GetAssembly());
            
        }

        public override void PostInitialize()
        {
            Helper.WithDbContextHelper.WithDbContext<MauiBoilerplateDbContext>(IocManager, RunMigrate);
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }

        public static void RunMigrate(MauiBoilerplateDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }


    }
}