using Core.DbView;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Db
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<TrainingCourse> TrainingCourse { get; set; }
		public DbSet<TrainingVideo> TrainingVideos { get; set; }
		public DbSet<Payments> Payments { get; set; }
		public DbSet<TestResult> TestResults { get; set; }
		public DbSet<CommonDropDown> CommonDropDown { get; set; }
		public DbSet<UserVerification> UserVerifications { get; set; }
		public DbSet<TestQuestions> TestQuestions { get; set; }
		public DbSet<AnswerOptions> AnswerOptions { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserLoginLog> UserLoginLogs { get; set; }
		public DbSet<PageStatistics> PageStatistics { get; set; }
		public DbSet<CompanySetting> CompanySettings { get; set; }
		public DbSet<View_PageStatisticsView> View_PageStatisticsViews { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
			});
			modelBuilder.Entity<ApplicationUser>(entity =>
			{
				entity.HasOne(e => e.Company)
					  .WithMany()
					  .HasForeignKey(e => e.CompanyId)
					  .OnDelete(DeleteBehavior.Restrict);
			});
			modelBuilder.Entity<View_PageStatisticsView>(eb =>
			{
				eb.HasNoKey();
				eb.ToView("View_PageStatisticsViews");
			});
		}
	}
}
