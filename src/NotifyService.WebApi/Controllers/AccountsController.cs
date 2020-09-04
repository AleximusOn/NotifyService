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

		[HttpPost(ApiRoutes.Accounts_Login)]
		public async Task<TokenDto> Login([FromBody] GetAccessTokenCommand command)
		{
			return await _mediator.Send(command);
		}

		[HttpPost(ApiRoutes.Accounts_Register)]
		public async Task<TokenDto> Register([FromBody] RegisterAccountCommand command)
		{
			return await _mediator.Send(command);
		}
	}
}