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
    public class HangfireDataContext : DbContext, IHangfireDataContext
    {
        private readonly ConnectionStrings _connectionStrings;

        public HangfireDataContext() { }

        public HangfireDataContext(ConnectionStrings connectionStrings) => (_connectionStrings) = (connectionStrings);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.HangfireConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void EnsureDbCreated()
        {
            this.Database.EnsureCreated();
        }
    }
}
