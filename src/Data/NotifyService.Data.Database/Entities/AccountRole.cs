using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NotifyService.Data.Database.Entities
{
	[Table("AccountRoles")]
	public class AccountRole : IdentityRole<Guid>
	{
	}
}