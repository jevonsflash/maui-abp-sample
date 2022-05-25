using Abp.Modules;
using Abp.Reflection.Extensions;
using MauiBoilerplate.EntityFrameworkCore;

namespace MauiBoilerplate
{
    [DependsOn(typeof(MauiBoilerplateEntityFrameworkCoreModule))]
    public class MauiBoilerplateModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MauiBoilerplateModule).GetAssembly());
        }

    }
}
