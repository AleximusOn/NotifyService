using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Data.Database
{
	public partial class BaseDbContext : IdentityDbContext<Account, AccountRole, Guid>
	{
		public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			NotifyServiceOnModelCreating(modelBuilder);

			modelBuilder.HasDefaultSchema("NotifyService");
		}
	}
}