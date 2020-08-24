﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using NotifyService.Common.Library.Configurations;
using NotifyService.Common.Library.Exceptions;
using NotifyService.Core.Domain.Commands;
using NotifyService.Core.Domain.Extensions;
using NotifyService.Core.Domain.Models;
using NotifyService.Data.Models.Entities;

namespace NotifyService.Core.Domain.CommandHandler
{
	public class GetAccessTokenHandler : IRequestHandler<GetAccessTokenCommand, TokenDto>
	{
		public async Task<TokenDto> Handle(GetAccessTokenCommand request, CancellationToken cancellationToken)
		{
			return await GenerateAccessToken(request.Username, request.Password);
		}

		private Task<TokenDto> GenerateAccessToken(string username, string password)
		{
			var identity = GetIdentity(username, password);
			if (identity == null)
			{
				throw new BadRequestException("Invalid username or password.");
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.Issuer,
				audience: AuthOptions.Audience,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(ConfigurationReader.Current.AuthOptions.Lifetime),
				signingCredentials: new SigningCredentials(ConfigurationReader.Current.AuthOptions.GetSymmetricSecurityKey(),
					SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new TokenDto
			{
				AccessToken = encodedJwt,
				Username = identity.Name
			};

			return Task.FromResult(response);
		}

		private readonly Account[] _accounts =
		{
			new Account {Login = "Admin", Role = "Admin"}
		};
		private ClaimsIdentity GetIdentity(string username, string password)
		{
			var account =
				_accounts.FirstOrDefault(a => a.Login.Equals(username, StringComparison.CurrentCultureIgnoreCase));
			if (account != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
				};
				ClaimsIdentity claimsIdentity =
					new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
						ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}
			
			// если пользователя не найдено
			return null;
		}
	}
}