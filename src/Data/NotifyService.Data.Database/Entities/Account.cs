using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NotifyService.Data.Database.Entities
{
	public class Account : IdentityUser<Guid>
	{
		public string DisplayName { get; set; }
	}
}