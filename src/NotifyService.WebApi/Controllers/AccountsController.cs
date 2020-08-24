using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotifyService.Core.Domain.Commands;
using NotifyService.Core.Domain.Models;
using NotifyService.WebApi.Consts;

namespace NotifyService.WebApi.Controllers
{
	[ApiController]
	[Route(ApiRoutes.Accounts)]
	public class AccountsController
	{
		private readonly IMediator _mediator;

		public AccountsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost(ApiRoutes.Accounts_Token)]
		public async Task<TokenDto> Token(GetAccessTokenCommand command)
		{
			return await _mediator.Send(command);
		}
	}
}