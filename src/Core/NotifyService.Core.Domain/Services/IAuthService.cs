using System.Threading.Tasks;
using NotifyService.Core.Domain.Models;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Core.Domain.Services
{
	public interface IAuthService
	{
		Task<TokenDto> GenerateJwtToken(Account account);
	}
}
