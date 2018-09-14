using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using CompanyNetCore.Model;
using System.Linq;

namespace CompanyNetCore.Repositories
{
    class EmployeeRepo
    {
        static EmployeeRepo _employeeRepo;
        public static EmployeeRepo GetInstance()
        {
            if (_employeeRepo == null)
                _employeeRepo = new EmployeeRepo();
            return _employeeRepo;
        }
        private EmployeeRepo()
        {

        }
        public List<Employee> Read()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
                {

                    conn.Open();
                    var result = conn.Query<Employee>("SELECT Id,Name,Birthdate,Salary,Gender FROM viEmployee").ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
        }
        public Employee Read(int Id)
        {
            try
            { 
                using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    var result = conn.QueryFirstOrDefault<Employee>("SELECT Id,Name,Birthdate,Salary,Gender FROM viEmployee where Id = @Id", param);
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
        }
        public Employee Create(Employee employee)
        {
            if (employee.Id != 0)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            var retval = InsertOrUpdate(employee);
            return retval;
        }
        public Employee Update(Employee employee)
        {
            if (employee.Id == 0)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            var retval = InsertOrUpdate(employee);
            return retval;
        }
        private Employee InsertOrUpdate(Employee employee)
        {
            try
            { 
                using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
                {
                    conn.Open();
                    var param = new DynamicParameters();
                    param.Add("@Id", employee.Id);
                    param.Add("@name", employee.Name);
                    param.Add("@birthdate", employee.Birthdate);
                    param.Add("@salary", employee.Salary);
                    int? gender;
                    if (employee.Gender == "männlich")
                    {
                        gender = 1;
                    }else if (employee.Gender == "weiblich")
                    {
                        gender = 2;
                    }
                    else if(employee.Gender == "kompliziert")
                    {
                        gender = 3;
                    }
                    else
                    {
                        gender = null;
                    }
                    param.Add("@gender", gender);
                    var result = conn.QueryFirstOrDefault<Employee>("spInsertOrUpdateEmployee", param, null, null, CommandType.StoredProcedure);
                    if (result == null)
                    {
                        throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
        }
        public Employee Delete(int Id)
        {
            try
            { 
                using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    var result = conn.QueryFirstOrDefault<Employee>("spDeleteEmployee", param, null, null, CommandType.StoredProcedure);
                    if (result == null)
                    {
                        throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.SQLERROR);
            }
        }
    }
}
