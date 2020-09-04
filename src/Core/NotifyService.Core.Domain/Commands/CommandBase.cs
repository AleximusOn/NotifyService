using MediatR;

namespace NotifyService.Core.Domain.Commands
{
	public class CommandBase<T> : IRequest<T> where T : class
	{
	}
}