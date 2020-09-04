using Autofac;
using MediatR;

namespace NotifyService.Core.Domain
{
	public class DomainModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
			
			builder.RegisterAssemblyTypes(ThisAssembly)
				.AsClosedTypesOf(typeof(IRequestHandler<,>))
				.InstancePerDependency();

			builder.Register<ServiceFactory>(context =>
			{
				var c = context.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});
		}
	}
}