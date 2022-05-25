using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MauiBoilerplate.Core.Configuration;
using MauiBoilerplate.Core.Localization;
using MauiBoilerplate.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MauiBoilerplate.Core
{
    [DependsOn(
       typeof(AbpAutoMapperModule))]
    public class MauiBoilerplateCoreModule : AbpModule
    {
        private readonly string development;

        public MauiBoilerplateCoreModule()
        {
            development = EnvironmentName.Development;

        }
        public override void PreInitialize()
        {
            LocalizationConfigurer.Configure(Configuration.Localization);

            Configuration.Settings.Providers.Add<CommonSettingProvider>();

            string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MauiBoilerplateConsts.LocalizationSourceName);

            var configuration = AppConfigurations.Get(documentsPath, development);
            var connectionString = configuration.GetConnectionString(MauiBoilerplateConsts.ConnectionStringName);

            var dbName = "mato.db";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MauiBoilerplateConsts.LocalizationSourceName, dbName);

            Configuration.DefaultNameOrConnectionString = String.Format(connectionString, dbPath);
            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MauiBoilerplateCoreModule).GetAssembly());
        }
    }
}
