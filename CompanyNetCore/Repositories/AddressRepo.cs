using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using CompanyNetCore.Model;
using System.Linq;

namespace CompanyNetCore.Repositories
{
    class AddressRepo
    { 
        public List<Model.Address> Read()
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                conn.Open();
                var result = conn.Query<Model.Address>("SELECT Id,Postcode,City,Street,Country FROM viAddress").ToList();
                return result;
            }

        }
        public Model.Address Read(int Id)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var result = conn.QueryFirstOrDefault<Model.Address>("SELECT Id,Postcode,City,Street,Country FROM viAddress where Id = @Id", param);
                return result;
            }
        }
        public Model.Address spInsertOrUpdate(Address address)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", address.Id);
                param.Add("@postcode", address.Postcode);
                param.Add("@city", address.City);
                param.Add("@street", address.Street);
                param.Add("@country", address.Country);
                var result = conn.QueryFirstOrDefault<Model.Address>("spInsertOrUpdateAddress", param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
        public Model.Address spDelete( int Id)
        {
            using (SqlConnection conn = new SqlConnection(CompanyNetCore.Properties.Resources.sqlConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var result = conn.QueryFirstOrDefault<Model.Address>("spDeleteAddress", param, null, null, CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
