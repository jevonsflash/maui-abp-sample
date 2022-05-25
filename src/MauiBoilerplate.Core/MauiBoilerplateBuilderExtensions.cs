﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Extensions;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Modules;
using Castle.Facilities.Logging;
using Castle.Windsor.MsDependencyInjection;
using MauiBoilerplate.Infrastructure.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace MauiBoilerplate.Core
{

    public static class MauiBoilerplateBuilderExtensions
    {

        public static MauiAppBuilder UseMauiBoilerplate<TStartupModule>(this MauiAppBuilder builder) where TStartupModule : AbpModule
        {
            var logCfgName = "log4net.config";
            var appCfgName = "appsettings.json";
            var dbName = "mato.db";

            string documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MauiBoilerplateConsts.LocalizationSourceName, logCfgName);
            string documentsPath2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MauiBoilerplateConsts.LocalizationSourceName, appCfgName);
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MauiBoilerplateConsts.LocalizationSourceName, dbName);

            InitConfig(logCfgName, documentsPath);
            InitConfig(appCfgName, documentsPath2);
            InitDataBase(dbName, dbPath);
            var _bootstrapper = AbpBootstrapper.Create<TStartupModule>(options =>
            {
                options.IocManager = new IocManager();
            });
            _bootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseAbpLog4Net().WithConfig(documentsPath));

            builder.Services.AddSingleton(_bootstrapper);
            WindsorRegistrationHelper.CreateServiceProvider(_bootstrapper.IocManager.IocContainer, builder.Services);

            return builder;
        }

        private static void InitConfig(string logCfgName, string documentsPath)
        {

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiBoilerplateBuilderExtensions)).Assembly;

            Stream stream = assembly.GetManifestResourceStream($"MauiBoilerplate.Core.{logCfgName}");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            if (DirFileHelper.IsExistFile(documentsPath))
            {
                var currentFileContent = DirFileHelper.ReadFile(documentsPath);
                var isSameContent = currentFileContent.ToMd5() == text.ToMd5();
                if (isSameContent)
                {
                    return;
                }
                DirFileHelper.CreateFile(documentsPath, text);

            }
            else
            {
                DirFileHelper.CreateFile(documentsPath, text);

            }
        }

        private static void InitDataBase(string dbName, string documentsPath)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MauiBoilerplateBuilderExtensions)).Assembly;
            //ef不需要我们写入db文件
            //Stream stream = assembly.GetManifestResourceStream($"MauiBoilerplate.Core.{dbName}");
            //StreamHelper.WriteStream(stream, documentsPath);

            var path = Path.GetDirectoryName(documentsPath);
            DirFileHelper.CreateDir(path);
        }
    }

}
