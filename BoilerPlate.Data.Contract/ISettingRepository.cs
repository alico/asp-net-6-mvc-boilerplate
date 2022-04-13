using BoilerPlate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoilerPlate.Data.Contract
{
    public interface ISettingRepository : IBaseRepository<Setting, Guid>
    {
        Task<Setting> GetByKey(short countryId, string key);
        Task<List<Setting>> GetByGroupId(short countryId, int groupId);
        Task<Setting> GetByValue(string value, short countryId);
    }
}
