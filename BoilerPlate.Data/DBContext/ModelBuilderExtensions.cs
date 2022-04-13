using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Utils;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BoilerPlate.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            CountrySeedData(modelBuilder);
            SettingSeedData(modelBuilder);
        }

        private static void SettingSeedData(ModelBuilder modelBuilder)
        {
            var countries = EnumHelper.GetEnumList<Countries>();
            List<Setting> settings = new List<Setting>();
            var i = 1;
            foreach (var country in countries)
            {
                settings.Add(new Setting()
                {
                    Id = Guid.NewGuid(),
                    Status = 1,
                    CountryId = (short)country,
                    GroupId = Constants.CampaignSettings,
                    Key = Constants.SeasonStartDate,
                    Value = DateTime.UtcNow.ToString(),
                    LastModifyDate = DateTime.Now,
                    CreationDate = DateTime.Now
                });

                settings.Add(new Setting()
                {
                    Id = Guid.NewGuid(),
                    Status = 1,
                    CountryId = (short)country,
                    GroupId = Constants.CampaignSettings,
                    Key = Constants.SeasonEndDateKey,
                    Value = DateTime.UtcNow.AddMonths(3).ToString(),
                    LastModifyDate = DateTime.Now,
                    CreationDate = DateTime.Now
                });

                settings.Add(new Setting()
                {
                    Id = Guid.NewGuid(),
                    Status = 1,
                    CountryId = (short)country,
                    GroupId = Constants.CampaignSettings,
                    Key = Constants.ClientSecret,
                    Value = CountryAPISecrets.Where(x => x.Key == country).Select(x => x.Value).FirstOrDefault(),
                    LastModifyDate = DateTime.Now,
                    CreationDate = DateTime.Now
                });
            }

            modelBuilder.Entity<Setting>().HasData(settings);
        }

        private static void CountrySeedData(ModelBuilder modelBuilder)
        {
            var countries = EnumHelper.GetEnumList<Countries>();
            var countryEntities = new List<Country>();
            foreach (var country in countries)
            {
                countryEntities.Add(new Country()
                {
                    Id = (short)country,
                    Name = country.ToString(),
                    LastModifyDate = DateTime.Now,
                    CreationDate = DateTime.Now
                });
            }
            modelBuilder.Entity<Country>().HasData(countryEntities);
        }


        private static List<KeyValuePair<Countries, string>> CountryAPISecrets = new List<KeyValuePair<Countries, string>>
        {
            new KeyValuePair<Countries, string>(Countries.UK,"a0d7d59a-526c-4b49-b351-4aef8e2d74d9"),
            new KeyValuePair<Countries, string>(Countries.Au,"0592f2c9-af25-44f4-9486-0daa68aaf7b7")
        };
    }
}
