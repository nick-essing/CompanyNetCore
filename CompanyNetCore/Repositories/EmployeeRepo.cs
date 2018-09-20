using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using CompanyNetCore.Model;
using System.Linq;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;

namespace CompanyNetCore.Repositories
{
    class EmployeeRepo : IRepository<Employee>
    {
        IDbContext _dbContext;
        IMessageHelper _messageHelper;
        public EmployeeRepo(IDbContext dbContext, IMessageHelper messageHelper)
        {
            _dbContext = dbContext;
            _messageHelper = messageHelper;
        }
        public List<Employee> Read()
        {
            try
            {
                List<Employee> retVal;
                var conn = _dbContext.GetCompany();
                string Select = "SELECT Id,Name,Birthdate,Salary,Gender FROM viEmployee;";
                using (conn)
                {
                    retVal = conn.Query<Employee>(Select).ToList();
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Employee Read(int Id)
        {
            try
            { 
                Employee retVal;
                var conn = _dbContext.GetCompany();
                string Select = "SELECT Id,Name,Birthdate,Salary,Gender FROM viEmployee WHERE Id = @Id;";
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    retVal = conn.QueryFirstOrDefault<Employee>(Select,param);
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Employee Create(Employee elm)
        {
            if (elm.Id != 0)
            {
                throw new RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retVal = InsertOrUpdate(elm);
            _messageHelper.SendIntercom($"Mitarbeiter Hinzugefügt: {retVal.Name}");
            return retVal;
        }
        public Employee Update(Employee elm)
        {
            if (elm.Id == 0 || Read(elm.Id) == null)
            {
                throw new RepoException<ResultType>(ResultType.INVALIDEARGUMENT);
            }
            var retVal = InsertOrUpdate(elm);
            _messageHelper.SendIntercom($"Mitarbeiter Geändert: {retVal.Name}");
            return retVal;
        }
        private Employee InsertOrUpdate(Employee employee)
        {
            try
            {
                Employee retVal;
                var conn = _dbContext.GetCompany();
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", employee.Id);
                    param.Add("@name", employee.Name);
                    param.Add("@birthdate", employee.Birthdate);
                    param.Add("@salary", employee.Salary);
                    int? gender;
                    if (employee.Gender == "männlich")
                    {
                        gender = 1;
                    }
                    else if (employee.Gender == "weiblich")
                    {
                        gender = 2;
                    }
                    else if (employee.Gender == "kompliziert")
                    {
                        gender = 3;
                    }
                    else
                    {
                        gender = null;
                    }
                    param.Add("@gender", gender);
                    retVal = conn.QueryFirstOrDefault<Employee>("spInsertOrUpdateEmployee", param, null, null, CommandType.StoredProcedure);
                }
                return retVal;
            }
            catch (Exception)
            {
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
        public Employee Delete(int Id)
        {
            try
            {
                Employee retVal;
                var conn = _dbContext.GetCompany();
                using (conn)
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    retVal = conn.QueryFirstOrDefault<Employee>("spDeleteEmployee", param, null, null, CommandType.StoredProcedure);
                }
                _messageHelper.SendIntercom($"Mitarbeiter Gelöscht: {retVal.Name}");
                return retVal;
            }
            catch (Exception)
            {
                throw new RepoException<ResultType>(ResultType.SQLERROR);
            }
        }
    }
}
