using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Utils;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

namespace BoilerPlate.Data
{
    public abstract class BaseDataContext<TUser, TRole, TKey> : IdentityDbContext<TUser,TRole, TKey>, IDataProtectionKeyContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ConnectionStrings _connectionStrings;

        public BaseDataContext() { }

        public BaseDataContext(ConnectionStrings connectionStrings) => (_connectionStrings) = (connectionStrings);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.MainConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Relations 

            #region ASP.NET Core Identity Tables
            modelBuilder.Entity<TUser>().ToTable("Users").HasKey(x => x.Id);

            modelBuilder.Entity<TRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<TKey>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<TKey>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<TKey>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasIndex(x => x.ProviderKey);
            });

            modelBuilder.Entity<IdentityRoleClaim<TKey>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<TKey>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            #endregion
        }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
