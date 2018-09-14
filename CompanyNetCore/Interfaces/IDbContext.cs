using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaTest.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
    }
}
