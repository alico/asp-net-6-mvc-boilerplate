using BoilerPlate.Data.Entities;
using BoilerPlate.Utils;
using BoilerPlate.Web.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoilerPlate.Service.Contract
{
    public interface ISettingService
    {
        Task<Setting> GetByKey(Countries country, string key, bool purge = false);
        Task<Setting> GetByKeyValue(string key, string value, bool purge = false);
        Task<List<Setting>> GetByGroupId(Countries country, int groupId, bool purge = false);
        Task<List<Setting>> GetAll(Countries country);
        Task Upsert(Countries country, string key, string value, short? groupId);
        Task Upsert(SettingsModel model);
        Task<Setting> GetByValue(Countries country, string value, bool purge = false);

        Task<bool> ValueExists(Countries country, string value);
    }
}
