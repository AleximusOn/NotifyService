using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NotifyService.Common.Library.Exceptions;
using NotifyService.Core.Domain.Commands;
using NotifyService.Core.Domain.Models;
using NotifyService.Core.Domain.Services;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Core.Domain.CommandHandler
{
	class RegisterAccountHandler : IRequestHandler<RegisterAccountCommand, TokenDto>
	{
		private readonly UserManager<Account> _userManager;
		private readonly SignInManager<Account> _signInManager;
		private readonly IAuthService _authService;

		public RegisterAccountHandler(UserManager<Account> userManager, SignInManager<Account> signInManager, IAuthService authService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		public async Task<TokenDto> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
		{
			var account = new Account
			{
				UserName = request.Email,
				Email = request.Email
			};

			var result = await _userManager.CreateAsync(account, request.Password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(account, false);
				return await _authService.GenerateJwtToken(account);
			}

			throw new BadRequestException("UNKNOWN_ERROR");
		}
	}
}
