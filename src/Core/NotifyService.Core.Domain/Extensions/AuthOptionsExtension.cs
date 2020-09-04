using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NotifyService.Common.Library.Configurations;

namespace NotifyService.Core.Domain.Extensions
{
	public static class AuthOptionsExtension
	{
		public static SymmetricSecurityKey GetSymmetricSecurityKey(this AuthOptions options)
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecretKey));
		}

		public static void AddBearerAuthentication(this IServiceCollection services, AuthOptions authOptions)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = AuthOptions.Issuer,
 
						// будет ли валидироваться потребитель токена
						ValidateAudience = true,
						// установка потребителя токена
						ValidAudience = AuthOptions.Audience,
						// будет ли валидироваться время существования
						ValidateLifetime = true,
 
						// установка ключа безопасности
						IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
						// валидация ключа безопасности
						ValidateIssuerSigningKey = true,
					};
				});
		}
	}
}