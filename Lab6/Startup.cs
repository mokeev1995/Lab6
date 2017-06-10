using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lab6
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

			services.AddRouting(options => { options.LowercaseUrls = true; });
			ConfigureEntityFramework(services);
			ConfigureDependencyInjection(services);
		}

		private void ConfigureEntityFramework(IServiceCollection services)
		{
			services.AddEntityFramework()
				.AddEntityFrameworkNpgsql()
				.AddDbContext<PostgresContext>(options =>
				{
					options.UseNpgsql(
						Configuration.GetConnectionString("Site")
					);
				});
		}

		private static void ConfigureDependencyInjection(IServiceCollection services)
		{
			services.AddLogging();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();
			ConfigureLoggers(loggerFactory);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStatusCodePagesWithRedirects("~/errors/{0}");

			ConfigureRoutes(app);
		}

		private void ConfigureLoggers(ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug(LogLevel.Debug);
		}

		private static void ConfigureRoutes(IApplicationBuilder app)
		{
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute("areaRoute",
					"{area:exists}/{controller=Home}/{action=Index}");
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}