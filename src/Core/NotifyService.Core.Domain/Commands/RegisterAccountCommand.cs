using System.ComponentModel.DataAnnotations;
using NotifyService.Core.Domain.Models;

namespace NotifyService.Core.Domain.Commands
{
    public class RegisterAccountCommand: CommandBase<TokenDto>
    {
        [Required]
        [EmailAddress]
		public string Email { get; set; }

		[Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
    }
}