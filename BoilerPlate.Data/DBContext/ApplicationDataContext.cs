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
    public class ApplicationDataContext : BaseDataContext<ApplicationUser, ApplicationRole, Guid>, IDataContext
    {
        public ApplicationDataContext():base() { }

        public ApplicationDataContext(ConnectionStrings connectionStrings):base(connectionStrings)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //PrimaryKeys
           
            modelBuilder.Entity<ApplicationLogs>().HasKey(x => x.Id);
            modelBuilder.Entity<Setting>().HasKey(x => x.Id);
            modelBuilder.Entity<Cache>().HasKey(x => x.Id);

            //Relations 
            modelBuilder.Seed();
        }

        public void EnsureDbCreated()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ApplicationLogs> ApplicationLogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
