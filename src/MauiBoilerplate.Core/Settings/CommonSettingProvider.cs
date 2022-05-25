using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Configuration;
using Abp.Localization;

namespace MauiBoilerplate.Core.Settings
{
    internal class CommonSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(CommonSettingNames.Theme, "Dark", L(CommonSettingNames.Theme), scopes: SettingScopes.All),
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }

}
