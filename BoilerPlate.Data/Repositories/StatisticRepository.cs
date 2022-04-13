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
    public class StatisticRepository : BaseRepository<BaseEntity<Guid>, Guid>, IStatisticRepository
    {
        public StatisticRepository(ConnectionStrings connectionStrings, IDataContext dataContext) : base(connectionStrings, dataContext)
        {

        }
    }
}
