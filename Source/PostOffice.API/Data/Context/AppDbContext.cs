using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Configurations;
using PostOffice.API.Data.Extensions;
using PostOffice.API.Data.Models;
using System;
using System.Data;
using System.Reflection.Emit;
using PostOffice.API.DTOs.User;

namespace PostOffice.API.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AreaConfig());
            builder.ApplyConfiguration(new PincodeConfig());
            builder.ApplyConfiguration(new OfficeBranchConfig());

            builder.ApplyConfiguration(new MoneyOrderConfig());
            builder.ApplyConfiguration(new MoneyScopeConfig());
            builder.ApplyConfiguration(new MoneyServicePriceConfig());

            builder.ApplyConfiguration(new ParcelOrderConfig());
            builder.ApplyConfiguration(new ParcelServiceConfig());
            builder.ApplyConfiguration(new ParcelServicePriceConfig());
            builder.ApplyConfiguration(new ParcelTypeConfig());
            builder.ApplyConfiguration(new WeightScopeConfig());

            builder.ApplyConfiguration(new ZoneTypeConfig());
            builder.ApplyConfiguration(new HistoryEmployeeConfig());
            builder.ApplyConfiguration(new OrderStatusConfig());

            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AppRoleConfig());

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);


           
            //Data seeding
            builder.Seed();                
        }

        public virtual DbSet<Area> Areas { get; set; }

        public virtual DbSet<MoneyOrder> MoneyOrders { get; set; }

        public virtual DbSet<MoneyScope> MoneyScopes { get; set; }

        public virtual DbSet<MoneyServicePrice> MoneyServices { get; set; }

        public virtual DbSet<OfficeBranch> OfficeBranches { get; set; }

        public virtual DbSet<ParcelOrder> ParcelOrders { get; set; }

        public virtual DbSet<ParcelService> ParcelServices { get; set; }

        public virtual DbSet<ParcelType> ParcelTypes { get; set; }

        public virtual DbSet<Pincode> Pincodes { get; set; }          

        public virtual DbSet<ParcelServicePrice> ServicePrices { get; set; }

        public virtual DbSet<TrackHistory> TrackHistories { get; set; }               

        public virtual DbSet<WeightScope> WeightScopes { get; set; }

        public virtual DbSet<ZoneType> ZoneTypes { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuss { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }

        public virtual DbSet<AppRole> AppRoles { get; set; }

        public virtual DbSet<HistoryEmployee> HistoryEmployees { get; set; }

    }
}
