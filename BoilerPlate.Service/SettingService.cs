using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Service
{
    public class SettingService : ISettingService
    {
        public readonly ISettingRepository _settingRepository;
        private readonly ICacheService _cacheService;

        public SettingService(ISettingRepository settingRepository, ICacheService cacheService)
        {
            _settingRepository = settingRepository;
            _cacheService = cacheService;
        }

        public async Task<List<Setting>> GetByGroupId(Countries country, int groupId, bool purge = false)
        {
            var cacheKey = $"SettingGroup-{(short)country}-{groupId}";
            return await _cacheService.Get(cacheKey, TimeSpan.FromMinutes(1),
                _settingRepository.GetByGroupId((short)country, groupId),
                purge);
        }

        public async Task<Setting> GetByKey(Countries country, string key, bool purge = false)
        {
            var cacheKey = $"{(short)country}-{key}";
            return await _cacheService.Get(cacheKey, TimeSpan.FromMinutes(10),
                _settingRepository.GetByKey((short)country, key),
               purge);
        }

        public async Task<Setting> GetByValue(Countries country, string value, bool purge = false)
        {
            return await _settingRepository.GetByValue(value, (short)country);
        }

        public async Task<bool> ValueExists(Countries country, string value)
        {
            return await _settingRepository.AnyAsync(x => x.CountryId == (short)country && x.Value == value);
        }

        public async Task<Setting> GetByKeyValue(string key, string value, bool purge = false)
        {
            var cacheKey = $"{key}-{value}";
            return await _cacheService.Get(cacheKey, TimeSpan.FromMinutes(10),
                _settingRepository.FirstOrDefaultAsync(x => x.Value == value && x.Key == key),
              purge);
        }

        public async Task<List<Setting>> GetAll(Countries country)
        {
            var settings = await _settingRepository.GetWhereAsync(x => x.CountryId == (short)country);
            if (settings != null)
                return settings.ToList();
            return null;
        }

        public async Task Upsert(Countries country, string key, string value, short? groupId)
        {
            var entity = await _settingRepository.GetByKey((short)country, key);

            if (entity != null)
            {
                entity.Value = value;
                entity.LastModifyDate = DateTime.Now;
                await _settingRepository.UpdateAsync(entity);
                await _settingRepository.CommitAsync();
            }
            else
            {
                await _settingRepository.AddAsync(new Setting()
                {
                    Key = key,
                    Value = value,
                    Status = 1,
                    GroupId = groupId,
                    CountryId = (short)country,
                    CreationDate = DateTime.Now,
                    LastModifyDate = DateTime.Now
                });
                await _settingRepository.CommitAsync();
            }
        }

        //TODO:Refactor it
        public async Task Upsert(SettingsModel model)
        {
            await Upsert(model.Country, Constants.ClientSecret, model.APISecret, null);
            await Upsert(model.Country, Constants.SeasonStartDate, model.StartDate.ToString(), null);
            await Upsert(model.Country, Constants.SeasonEndDateKey, model.EndDate.ToString(), null);
        }
    }
}
