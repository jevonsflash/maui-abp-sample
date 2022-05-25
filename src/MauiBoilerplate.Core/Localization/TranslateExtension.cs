﻿using System;
using System.Reflection;
using System.Resources;
using Abp.Domain.Services;
using Abp.Localization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace MauiBoilerplate.Core
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : DomainService, IMarkupExtension
    {
        public TranslateExtension()
        {
            LocalizationSourceName = MauiBoilerplateConsts.LocalizationSourceName;

        }
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";
            var translation = L(Text);          
            return translation;
        }



    }
}
