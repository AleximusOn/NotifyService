using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotifyService.Common.Library.Configurations;
using NotifyService.Core.Domain.Extensions;
using NotifyService.Data.Database;
using NotifyService.Data.Database.Entities;

namespace NotifyService.WebApi
{
	public class Startup
	{
		public Startup(IWebHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			this.Configuration = builder.Build();
			
			var appConfiguration = GetAppConfigSection().Get<AppConfiguration>();
			ConfigurationReader.SetCurrent(appConfiguration);
		}

		public IConfigurationRoot Configuration { get; private set; }
		public ILifetimeScope AutofacContainer { get; private set; }


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<AppConfiguration>(GetAppConfigSection());

			services.AddDbContext<BaseDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentityCore<Account>()
				.AddRoles<AccountRole>()
				.AddEntityFrameworkStores<BaseDbContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();
			
			services.AddControllers();
			
			services.AddBearerAuthentication(ConfigurationReader.Current.AuthOptions);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			AutofacContainer = app.ApplicationServices.GetAutofacRoot();
			
			LoggerFactory.Create(builder => builder.AddConsole().AddDebug());

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			var optionsMonitor = (IOptionsMonitor<AppConfiguration>)app.ApplicationServices
				.GetService(typeof(IOptionsMonitor<AppConfiguration>));
			optionsMonitor.OnChange(ConfigurationReader.SetCurrent);
		}
		
		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new WebApiModule());
		}
		
		private IConfiguration GetAppConfigSection()
		{
			return Configuration.GetSection(AppConfiguration.SectionName);
		}
	}
}
