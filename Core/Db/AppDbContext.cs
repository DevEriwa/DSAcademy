﻿using Core.Models;
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
		public DbSet<UserVerification> userVerifications { get; set; }
	}
}
