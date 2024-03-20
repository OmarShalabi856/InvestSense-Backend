using InvestSense_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvestSense_API.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		
		public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{


		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

			builder.Entity<Portfolio>()
				.HasOne(x => x.AppUser)
				.WithMany(x => x.Portfolios)
				.HasForeignKey(x => x.AppUserId);


			builder.Entity<Portfolio>()
				.HasOne(x => x.Stock)
				.WithMany(x => x.Portfolios)
				.HasForeignKey(x => x.StockId);

			List<IdentityRole> roles = new List<IdentityRole>()
			{
				new IdentityRole()
				{
					Name="Admin",
					NormalizedName="ADMIN",
				},
				new IdentityRole()
				{
					Name="User",
					NormalizedName="USER",
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

		}
		

		public DbSet<Stock> Stock { get; set; }
		public DbSet<Comment> Comment { get; set; }

		public DbSet<Portfolio> Portfolio { get; set; }


	}
}
