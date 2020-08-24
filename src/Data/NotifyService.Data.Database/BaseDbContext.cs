using Microsoft.EntityFrameworkCore;
using NotifyService.Data.Database.Entities;

namespace NotifyService.Data.Database
{
	public class BaseDbContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountRole> AccountRoles { get; set; }

		public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("NotifyService");
		}
	}
}