﻿using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using MauiBoilerplate.Core;

namespace MauiBoilerplate
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("FontAwesome.ttf", "FontAwesome");
				})
				.UseMauiBoilerplate<MauiBoilerplateModule>();
			return builder.Build();
		}
	}
}