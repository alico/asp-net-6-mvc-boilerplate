﻿using BoilerPlate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoilerPlate.Data.Contract
{
    public interface IStatisticRepository : IBaseRepository<BaseEntity<Guid>, Guid>
    {
      
    }
}
