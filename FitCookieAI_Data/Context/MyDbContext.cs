﻿using FitCookieAI_Data.Entities.AdminRelated;
using FitCookieAI_Data.Entities.Others;
using FitCookieAI_Data.Entities.UserRelated;
using GlobalVariables.Encription;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCookieAI_Data.Context
{
	public class MyDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<AdminStatus> AdminStatuses { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentPlan> PaymentPlans { get; set; }
		public DbSet<PaymentPlanFeature> PaymentPlanFeatures { get; set; }
		public DbSet<PaymentPlanToUser> PaymentPlansToUsers { get; set; }
		public DbSet<RefreshUserToken> RefreshUserTokens { get; set; }
        public DbSet<RefreshAdminToken> RefreshAdminTokens { get; set; }
		public DbSet<GeneratedPlan> GeneratedPlans { get; set; }
		public DbSet<PasswordRecoveryToken> PasswordRecoveryTokens { get; set; }

		public MyDbContext()
		{
			Users = this.Set<User>();
			Admins = this.Set<Admin>();
			AdminStatuses = this.Set<AdminStatus>();
			Payments = this.Set<Payment>();
			PaymentPlans = this.Set<PaymentPlan>();
			PaymentPlanFeatures = this.Set<PaymentPlanFeature>();
			PaymentPlansToUsers = this.Set<PaymentPlanToUser>();
			RefreshAdminTokens = this.Set<RefreshAdminToken>();
			RefreshUserTokens = this.Set<RefreshUserToken>();
			GeneratedPlans = this.Set<GeneratedPlan>();
			PasswordRecoveryTokens = this.Set<PasswordRecoveryToken>();

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("FitCookieAI");

            // Seeding AdminStatus
            modelBuilder.Entity<AdminStatus>().HasData(new AdminStatus
			{
				Id = 1, 
				Name = "Developer" 
			});

			modelBuilder.Entity<AdminStatus>().HasData(new AdminStatus
			{
				Id = 2,
				Name = "Administrator"
			});

			modelBuilder.Entity<AdminStatus>().HasData(new AdminStatus
			{
				Id = 3,
				Name = "Unverified"
			});

			// Seeding Admin
			modelBuilder.Entity<Admin>().HasData(new Admin
			{
				Id = 1, 
				Email = "admin@mail.com",
				Password = StringCipher.Encrypt("123456", EncriptionVariables.PasswordEncriptionKey), 
				FirstName = "Admin",
				LastName = "User",
				DOB = DateTime.Now, 
				Gender = "Male",
				StatusId = 1, 
				ProfilePhotoName = ""
			});
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //User ID=postgres;Password=rexibexi1;Host=localhost;Port=5432;Database=FitCookieAI_db
            //Server=4.234.153.155;Database=fitcookie;Port=5432;User ID=postgres;Password=fitcookieAI2023
            optionsBuilder.UseNpgsql("Server=4.234.153.155;Database=fitcookie;Port=5432;User ID=postgres;Password=fitcookieAI2023");
		}
	}
}
