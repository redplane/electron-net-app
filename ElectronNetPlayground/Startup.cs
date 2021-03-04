using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using ElectronNetPlayground.ViewModels;

namespace ElectronNetPlayground
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			Electron.IpcMain.On("hello-world", (args) =>
			{
				var model = new HelloWorldViewModel();
				model.Title = "This is hello world title";
				model.Content = "This is hello world content";

				var mainWindow = Electron.WindowManager.BrowserWindows.FirstOrDefault();
				Electron.IpcMain.Send(mainWindow, "asynchronous-reply", "pong");
			});

			// Open the Electron-Window here
			var browserWindowOptions = new BrowserWindowOptions();
			browserWindowOptions.TitleBarStyle = TitleBarStyle.hidden;
			browserWindowOptions.AutoHideMenuBar = true;
			browserWindowOptions.WebPreferences = new WebPreferences();
			browserWindowOptions.WebPreferences.DevTools = true;

			app.UseRouting();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});


			Task.Run(async () =>
			{
				var browserWindow = await Electron.WindowManager.CreateWindowAsync(browserWindowOptions);
				browserWindow.LoadURL("http://localhost:4200");
				browserWindow.Show();
			});

		}
	}
}
