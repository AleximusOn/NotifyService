using System;
using System.Text;

namespace NotifyService.Common.Library.Configurations
{
	public class AuthOptions
	{
		/// <summary>
		/// Издатель токена
		/// </summary>
		public const string Issuer = "NotifyService.Auth";
		
		/// <summary>
		/// Потребитель токена
		/// </summary>
		public const string Audience = "NotifyService.Auth.Client";
		
		/// <summary>
		/// ключ для шифрации
		/// </summary>
		public string SecretKey { get; set; } = "l4r62Ui6k8cRz4ESUztgzPdNRns4MkSGUuMX0kj0nW3iBa1csbhA3joXjKlU";
		
		/// <summary>
		/// Время жизни токена
		/// </summary>
		public TimeSpan Lifetime { get; set; } = TimeSpan.FromHours(1);
	}
}