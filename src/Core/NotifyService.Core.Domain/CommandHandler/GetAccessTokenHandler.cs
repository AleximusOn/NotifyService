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
	public class GetAccessTokenHandler : IRequestHandler<GetAccessTokenCommand, TokenDto>
	{
		private readonly IAuthService _authService;
		private readonly UserManager<Account> _userManager;
		private readonly SignInManager<Account> _signInManager;

		public GetAccessTokenHandler(IAuthService authService, UserManager<Account> userManager, SignInManager<Account> signInManager)
		{
			_authService = authService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<TokenDto> Handle(GetAccessTokenCommand request, CancellationToken cancellationToken)
		{
			return await GenerateAccessToken(request.Email, request.Password);
		}

		private async Task<TokenDto> GenerateAccessToken(string email, string password)
		{
			var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
			if (result.Succeeded)
			{
				var user = await _userManager.FindByEmailAsync(email);
				return await _authService.GenerateJwtToken(user);
			}
			
			throw new BadRequestException("Invalid username or password.");
		}
	}
}