using Autofac;
using NotifyService.Common.Library;
using NotifyService.Core.Domain;
using NotifyService.Data.Models;

namespace NotifyService.WebApi
{
	public class WebApiModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			RegisterModules(builder);
		}

		private void RegisterModules(ContainerBuilder builder)
		{
			builder.RegisterModule(new CommonLibraryModule());
			builder.RegisterModule(new DomainModule());
			builder.RegisterModule(new DataModelsModule());
		}
	}
}