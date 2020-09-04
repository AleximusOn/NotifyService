using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NotifyService.Common.Library.Configurations;
using NotifyService.Core.Domain.Extensions;
using NotifyService.Core.Domain.Models;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Core.Domain.Services
{
	public class AuthService : IAuthService
	{
		public async Task<TokenDto> GenerateJwtToken(Account account)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, account.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
			};

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.Issuer,
				audience: AuthOptions.Audience,
				notBefore: now,
				claims: claims,
				expires: now.Add(ConfigurationReader.Current.AuthOptions.Lifetime),
				signingCredentials: new SigningCredentials(ConfigurationReader.Current.AuthOptions.GetSymmetricSecurityKey(),
					SecurityAlgorithms.HmacSha256));
			
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return new TokenDto(account.Email, encodedJwt);
		}
	}
}
