﻿using System;
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
                var result = conn.QueryFirstOrDefault<Employee>("SELECT Id,Name,Birthdate,Salary,Gender FROM viEmpolyee where Id = @Id", param);
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
                param.Add("@gender", employee.Gender);
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