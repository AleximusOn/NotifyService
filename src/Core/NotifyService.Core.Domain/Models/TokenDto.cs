namespace NotifyService.Core.Domain.Models
{
	public class TokenDto
	{
		public TokenDto(string username, string accessToken)
		{
			Username = username;
			AccessToken = accessToken;
		}

		public string Username { get; set; }
		public string AccessToken { get; set; }
	}
}