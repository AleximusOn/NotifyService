using System.ComponentModel.DataAnnotations;
using MediatR;
using NotifyService.Core.Domain.Models;

namespace NotifyService.Core.Domain.Commands
{
	public class GetAccessTokenCommand : CommandBase<TokenDto>
	{
		[Required]
		public string Username { get; set; }
		
		[Required]
		public string Password { get; set; }
	}
}