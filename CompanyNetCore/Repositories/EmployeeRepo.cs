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
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                conn.Open();
                var result = conn.Query<Employee>("SELECT Id,Name,Birthdate,Salary,Gender FROM viEmployee").ToList();
                return result;
            }
        }
        public Employee Read(int Id)
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
        public Employee spInsertOrUpdate(Employee employee)
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
                return result;
            }
        }
        public Employee spDelete( int Id)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var result = conn.QueryFirstOrDefault<Employee>("spDeleteEmployee", param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
