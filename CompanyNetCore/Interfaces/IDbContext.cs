﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
    }
}
