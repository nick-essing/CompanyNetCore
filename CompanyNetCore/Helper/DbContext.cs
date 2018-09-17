using CompanyNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Options;
using CompanyNetCore.Model;
using System.Data.SqlClient;

namespace CompanyNetCore.Helper
{
    class DbContext : IDbContext
    {
        private readonly DbSettings _settings;
        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }
        public IDbConnection GetCompany()
        {
            var con = new SqlConnection(_settings.Company);
            con.Open();
            return con;
        }
    }
}