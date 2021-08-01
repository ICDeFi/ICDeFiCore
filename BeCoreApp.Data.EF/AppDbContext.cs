﻿using BeCoreApp.Data.EF.Configurations;
using BeCoreApp.Data.EF.Extensions;
using BeCoreApp.Data.Entities;
using BeCoreApp.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BeCoreApp.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Language> Languages { set; get; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<MenuItem> MenuItems { set; get; }
        public DbSet<BlogCategory> BlogCategories { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WholePrice> WholePrices { get; set; }
        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<BeCoreApp.Data.Entities.Type> Types { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ClassifiedCategory> ClassifiedCategories { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<ProjectLibrary> ProjectLibraries { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<UserBonus> AppUserBonuses { get; set; }
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public DbSet<Invest> Invests { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Support> Supports { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<WalletTransfer> WalletTransfers { get; set; }

        public DbSet<LuckyRoom> LuckyRooms { get; set; }
        public DbSet<AppUserLuckyRoom> AppUserLuckyRooms { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity Config

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins")
                .HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
                .HasKey(x => x.UserId);

            #endregion

            builder.AddConfiguration(new TagConfiguration());
            builder.AddConfiguration(new BlogTagConfiguration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterConfiguration());
            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new WardConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new AdvertistmentPositionConfiguration());
            builder.AddConfiguration(new AdvertistmentPageConfiguration());
            builder.AddConfiguration(new FunctionConfiguration());
            builder.AddConfiguration(new AnnouncementConfiguration());
            builder.AddConfiguration(new StreetConfiguration());
            builder.AddConfiguration(new EnterpriseConfiguration());
            builder.AddConfiguration(new ProjectConfiguration());
            builder.AddConfiguration(new BlogConfiguration());
            builder.AddConfiguration(new BlogCategoryConfiguration());

            //base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                        changedOrAddedItem.DateCreated = DateTime.Now;

                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
