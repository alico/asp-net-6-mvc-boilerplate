using BoilerPlate.Data.Contract;
using BoilerPlate.Data.Entities;
using BoilerPlate.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoilerPlate.Data
{
    public class SettingRepository : BaseRepository<Setting, Guid>, ISettingRepository
    {
        public SettingRepository(ConnectionStrings connectionStrings, IDataContext dataContext) : base(connectionStrings, dataContext)
        {

        }

        public async Task<List<Setting>> GetByGroupId(short countryId, int groupId)
        {
            return await _dataContext.Set<Setting>().Where(x => x.GroupId == groupId && x.CountryId == countryId).ToListAsync();
        }

        public async Task<Setting> GetByKey(short countryId, string key)
        {
            return await _dataContext.Set<Setting>().Where(x => x.Key == key && x.CountryId == countryId).FirstOrDefaultAsyncNoLock();
        }

        public async Task<Setting> GetByValue(string value, short countryId)
        {
            return await _dataContext.Set<Setting>().Where(x => x.Value == value && x.CountryId == countryId).FirstOrDefaultAsyncNoLock();
        }

    }
}
