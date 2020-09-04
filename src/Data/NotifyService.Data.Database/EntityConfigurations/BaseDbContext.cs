using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Data.Database
{
	public partial class BaseDbContext
	{
		private void NotifyServiceOnModelCreating(ModelBuilder modelBuilder)
		{
			ConfigureIdentity(modelBuilder);
		}

		private void ConfigureIdentity(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>()
				.ToTable("Accounts");

			modelBuilder.Entity<AccountRole>()
				.ToTable("AccountRoles");

			modelBuilder.Entity<IdentityUserRole<Guid>>()
				.ToTable("AccountAccountRoles");

			modelBuilder.Entity<IdentityUserClaim<Guid>>()
				.ToTable("AccountClaims");

			modelBuilder.Entity<IdentityUserLogin<Guid>>()
				.ToTable("AccountLogins");

			modelBuilder.Entity<IdentityUserToken<Guid>>()
				.ToTable("AccountTokens");

			modelBuilder.Entity<IdentityRoleClaim<Guid>>()
				.ToTable("AccountRoleClaims");
		}
	}
}
